using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvokatskiPortal.Data;
using AdvokatskiPortal.Models;
using AdvokatskiPortal.Models.View;
using AdvokatskiPortal.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvokatskiPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvokatController : ControllerBase
    {
        private readonly PortalAdvokataDbContext _context;
        readonly UserManager<IdentityUser> userManager;
        readonly SignInManager<IdentityUser> signInManager;
        private IAdvokatRepo advokatRepo;
        public AdvokatController(PortalAdvokataDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.advokatRepo = new AdvokatRepo(_context);
        }
        [HttpGet]
        public IEnumerable<Advokat> GetAdvokati()
        {
            var advokati = advokatRepo.GetAll();

            return advokati;
        }
        [HttpGet("getNewNostifiation")]
        public int getNewNostifiation()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Advokats.Single(x => x.Idenity.Id == cliems.Value);

            var noviSlucajevi = _context.SlucajAdvokats.Where(s => s.Advokat.Id == ulogovaniKorisnik.Id && s.isRead == false);
            
            return noviSlucajevi.Count();
        }
        [HttpGet("getUgovorsForAdvokat")]
        public IEnumerable<SlucajAdvokat> getUgovorsForAdvokatAsync()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Advokats.Single(x => x.Idenity.Id == cliems.Value);
            
            var sviSlucajiAdvokata = _context.SlucajAdvokats.Where(a => a.Advokat.Id == ulogovaniKorisnik.Id).Include(t=>t.Slucaj.Cenovniks).Include(s=>s.Slucaj).ThenInclude( c => c.Korisnik);
                                   
            return sviSlucajiAdvokata;
        }
        [HttpGet("getSlucajNaCekanju")]
        public IEnumerable<SlucajAdvokat> getSlucajNaCekanju()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Advokats.Single(x => x.Idenity.Id == cliems.Value);
            
            var sviSlucajiAdvokata = _context.SlucajAdvokats.Where(a => a.Advokat.Id == ulogovaniKorisnik.Id).Include(t => t.Slucaj.Cenovniks).Include(s => s.Slucaj).ThenInclude(c => c.Korisnik).Where(q=>q.SlucajStatusId == 1);
            
            return sviSlucajiAdvokata;
        }
       
        [HttpGet("getSlucajiPrihvaceni")]
        public IEnumerable<SlucajAdvokat> getSlucajiPrihvaceni()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Advokats.Single(x => x.Idenity.Id == cliems.Value);
            
            var sviSlucajiAdvokata = _context.SlucajAdvokats.Where(a => a.Advokat.Id == ulogovaniKorisnik.Id).Include(t => t.Slucaj.Cenovniks).Include(s => s.Slucaj).ThenInclude(c => c.Korisnik).Where(q=>q.SlucajStatusId == 4);

            return sviSlucajiAdvokata;
        }

        /// <summary>
        /// odobrenje odbijanje i prihvatanje slucaja
        /// </summary>
        /// <param name="slucajAdvokat"></param>
        /// <returns></returns>
        [HttpPost("postavljanjeNoveCeneOdAdvokata")]
        public async Task<IActionResult> postavljanjeNoveCeneOdAdvokata([FromBody] postNewCenovnikFromAdvokatVM noviCenovnikVM)
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Advokats.Single(x => x.Idenity.Id == cliems.Value);
            var cenovnik = new Cenovnik
            {
                IdenityId = cliems.Value,
                SlucajId = noviCenovnikVM.SlucajId,
                kolicina = noviCenovnikVM.Cenovnik.kolicina,
                komentar = noviCenovnikVM.Cenovnik.komentar,
                vrstaPlacanja = noviCenovnikVM.Cenovnik.vrstaPlacanja,
                StatusId = 1
            };
            _context.Cenovniks.Add(cenovnik);
            _context.SaveChangesAsync();

            return Ok(cenovnik);
        }

        [HttpPut("prihvatanjeSlucajaAdvokata")]
        public async Task<IActionResult> prihvatanjeSlucajaAdvokata( [FromBody] SlucajAdvokat slucajAdvokat)
        {

            slucajAdvokat.SlucajStatusId = 2;
            _context.Entry(slucajAdvokat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("odbijanjeSlucajaAdvokata")]
        public async Task<IActionResult> odbijanjeSlucajaAdvokata( [FromBody] SlucajAdvokat slucajAdvokat)
        {

            slucajAdvokat.SlucajStatusId = 3;
            _context.Entry(slucajAdvokat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}