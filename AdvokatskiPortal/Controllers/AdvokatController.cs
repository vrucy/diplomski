using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvokatskiPortal.Data;
using AdvokatskiPortal.Models;
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
            
         var y = noviSlucajevi.Count();
            return y;
        }
        [HttpGet("getUgovorsForAdvokat")]
        public IEnumerable<SlucajAdvokat> getUgovorsForAdvokatAsync()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Advokats.Single(x => x.Idenity.Id == cliems.Value);

            //var sviSlucajiAdvokata = _context.SlucajAdvokats.Where(a => a.Advokat.Id == ulogovaniKorisnik.Id);
            
            var sviSlucajiAdvokata = _context.SlucajAdvokats.Where(a => a.Advokat.Id == ulogovaniKorisnik.Id).Include(t=>t.Slucaj.Cenovniks).Include(s=>s.Slucaj).ThenInclude( c => c.Korisnik);
                                   
            
            return sviSlucajiAdvokata;
        }
    }
}