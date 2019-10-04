using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvokatskiPortal.Data;
using AdvokatskiPortal.Models;
using AdvokatskiPortal.Models.View;
using Microsoft.AspNetCore.Authorization;
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
        public AdvokatController(PortalAdvokataDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet("getAllMajstori")]
        public IEnumerable<Majstor> getAllMajstori()
        {
            var xo = _context.Majstors.Include(x => x.MajstorKategorijes);
            return _context.Majstors.Include(x => x.MajstorKategorijes);
        }

        [HttpGet("getNewNostifiation")]
        public async Task<IActionResult> getNewNostifiation()
        {
            try
            {
                var cliems = User.Claims.First();
                var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);
                var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Majstor.Id == ulogovaniKorisnik.Id && s.isRead == false);

                return Ok(noviSlucajevi);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw e;
            }
            
        }
        [HttpPut("putNewNostifiationRead")]
        public async Task<IActionResult> putNewNostifiationRead([FromBody] SlucajMajstor[] nostification)
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);
            // potrebno prebaciti isRead na true;
            var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Majstor.Id == ulogovaniKorisnik.Id && s.isRead == false).Include(s => s.Slucaj);
            foreach (var item in noviSlucajevi)
            {
                item.isRead = true;
                _context.Entry(item).State = EntityState.Modified;
            };

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("getUgovorsForAdvokat")]
        public IEnumerable<SlucajMajstor> getUgovorsForAdvokatAsync()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);
            // && a.Slucaj.cenovnik != 1 treba staviti kad se veza promeni cenovnik prema slucaju 1:1
            var sviSlucajiAdvokata = _context.SlucajMajstors.Where(a => a.Majstor.Id == ulogovaniKorisnik.Id ).Include(t=>t.Slucaj.Cenovnik).Include(sl=>sl.Slucaj.Slike).Include(s => s.Slucaj).ThenInclude( c => c.Korisnik).ThenInclude(i => i.Idenity);
                                   
            return sviSlucajiAdvokata;
        }
        [HttpGet("getSlucajNaCekanju")]
        public IEnumerable<SlucajMajstor> getSlucajNaCekanju()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);
            
            var sviSlucajiAdvokata = _context.SlucajMajstors.Where(a => a.Majstor.Id == ulogovaniKorisnik.Id).Include(t => t.Slucaj.Cenovnik).Include(s => s.Slucaj).ThenInclude(c => c.Korisnik).Where(q=>q.SlucajStatusId == 1);
            
            return sviSlucajiAdvokata;
        }
       
        [HttpGet("getSlucajiPrihvaceni")]
        public IEnumerable<SlucajMajstor> getSlucajiPrihvaceni()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);
            
            var sviSlucajiAdvokata = _context.SlucajMajstors.Where(a => a.Majstor.Id == ulogovaniKorisnik.Id).Include(t => t.Slucaj.Cenovnik).Include(s => s.Slucaj).ThenInclude(c => c.Korisnik).Where(q=>q.SlucajStatusId == 4);

            return sviSlucajiAdvokata;
        }

        /// <summary>
        /// odobrenje odbijanje i prihvatanje slucaja
        /// </summary>
        /// <param name="slucajAdvokat"></param>
        /// <returns></returns>
        /// POTREBNO JE NAPRAVITI U CLIJENTU 2 POZIVA NA KLIKOM EDIT 1 DA SE NAPRAVI NOVI CENOVNIK A DRUGI DA SE EDITUJE STANJE TOG SLUCAJA NA ODGOVOR ADVOKAT
        [HttpPost("postavljanjeNoveCeneOdAdvokata")]
        public async Task<IActionResult> postavljanjeNoveCeneOdAdvokata([FromBody] postNewCenovnikFromAdvokatVM noviCenovnikVM)
        {
            
                var cliems = User.Claims.First();
                var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);

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

                await _context.SaveChangesAsync();

                return Ok(cenovnik);
           
        }
        [HttpPut("prepravkaCeneOdAdvokata")]
        public async Task<IActionResult> prepravkaCeneOdAdvokata ([FromBody] Cenovnik noviCenovnikVM)
        {
            
            await _context.SaveChangesAsync();
            try
            {
                var cliems = User.Claims.First();
                noviCenovnikVM.IdenityId = cliems.Value;
                noviCenovnikVM.StatusId = 1;
                _context.Entry(noviCenovnikVM).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }

            return Ok();

        }
        [HttpPut("prihvatanjeSlucajaAdvokata")]
        public async Task<IActionResult> prihvatanjeSlucajaAdvokata( [FromBody] SlucajMajstor slucajMajstor)
        {
            if (slucajMajstor.SlucajStatusId == 1 || slucajMajstor.SlucajStatusId == 3 || slucajMajstor.SlucajStatusId == 6)
            {
                slucajMajstor.SlucajStatusId = 4;
                _context.Entry(slucajMajstor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            } else
            {
                return BadRequest();
            }
        }
        [HttpPut("prepravkaSlucajaAdvokata")]
        public async Task<IActionResult> prepravkaSlucajaAdvokata([FromBody] SlucajMajstor slucajMajstor)
        {

            slucajMajstor.SlucajStatusId = 7;
            _context.Entry(slucajMajstor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("odbijanjeSlucajaAdvokata")]
        public async Task<IActionResult> odbijanjeSlucajaAdvokata( [FromBody] SlucajMajstor slucajMajstor)
        {

            slucajMajstor.SlucajStatusId = 3;
            _context.Entry(slucajMajstor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
        [Authorize(Policy = "AdminAdvokat")]
        [HttpPost("postKategorija")]
        public async Task<IActionResult> postKategorija([FromBody] Kategorija kategorija)
        {
            _context.Kategorijas.Add(kategorija);

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("postPodKategorija")]
        public async Task<IActionResult> PodKategorija([FromBody] Kategorija podkategorija)
        {
            _context.Kategorijas.Add(podkategorija);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("getAllKategorija")]
        public IEnumerable<Kategorija> GetKategorijas()
        {
            return _context.Kategorijas;
        }

    }
}