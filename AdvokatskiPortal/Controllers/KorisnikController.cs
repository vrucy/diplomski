using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdvokatskiPortal.Data;
using AdvokatskiPortal.Models;
using Microsoft.AspNetCore.Identity;
using AdvokatskiPortal.Models.View;
using System.Net.Http;

namespace AdvokatskiPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly PortalAdvokataDbContext _context;
        readonly UserManager<IdentityUser> userManager;
        //readonly UserManager<ApplicationUser> _userManager;
        public KorisnikController(PortalAdvokataDbContext context, UserManager<IdentityUser> userManager/*, UserManager<ApplicationUser> manager*/)
        {
            _context = context;
            this.userManager = userManager;
        }

        [HttpGet("getAllSlucajForKorisnik")]
        public IEnumerable<Slucaj> getAllSlucajForKorisnik()
        {
            try
            {
                var x = User.Claims.FirstOrDefault().Value;
                var korsinikSlucajevi = _context.Slucajs.Where(k => k.Korisnik.Idenity.Id == x);
             
                return korsinikSlucajevi;
            }
            catch (Exception e)
            {

                throw;
            }
            
        }
        // GET: api/Korisnik/5
        [HttpGet("getAllSlucajAdvokatForKorisnik")]
        public IEnumerable<SlucajAdvokat> getAllSlucajAdvokatForKorisnik()
        {
            var x = User.Claims.FirstOrDefault().Value;
            var korsinikSlucajevi = _context.Slucajs.Where(k => k.Korisnik.Idenity.Id == x).Include(k => k.Korisnik).ThenInclude(i => i.Idenity);
            var test = new List<SlucajAdvokat>();
            foreach (var item in korsinikSlucajevi)
            {
                test.AddRange(_context.SlucajAdvokats.Where(d => d.SlucajId == item.Id).Include(a => a.Advokat.Idenity).Include(s => s.Slucaj).ThenInclude(c => c.Cenovniks));
            }
            return test;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKorisnik([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var korisnik = await _context.Korisniks.FindAsync(id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return Ok(korisnik);
        }
        [HttpGet("GetUgovorsForKorisnik")]
        public IEnumerable<SlucajAdvokat> GetUgovorsForKorisnik()
        {
            
            var x = User.Claims.FirstOrDefault().Value;
            var korsinikSlucajevi = _context.Slucajs.Where(k => k.Korisnik.Idenity.Id == x).Select(i => i.Id);
            var f = _context.SlucajAdvokats.Where(d => d.SlucajId == korsinikSlucajevi.FirstOrDefault()).Include(a => a.Advokat.Idenity).Include(s => s.Slucaj).ThenInclude(c => c.Cenovniks);

            return f;

        }
              
        // POST: api/Korisnik
        [HttpPost]
        public async Task<IActionResult> PostKorisnik([FromBody] Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Korisniks.Add(korisnik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKorisnik", new { id = korisnik.Id }, korisnik);
        }

        [HttpPost("kreiranjeSlucaja")]
        public async Task<IActionResult> kreiranjeSlucaja([FromBody] Slucaj slucaj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cliems = User.Claims.First();
            var q = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);
            slucaj.Korisnik = q ;
            slucaj.KorisnikId = q.Id;
            _context.Slucajs.Include(y => y.Korisnik.Id == q.Id);
            _context.Slucajs.Add(slucaj);
            var c = _context.Slucajs.Find(slucaj.Id);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSlucaj", new { id = slucaj.Id }, slucaj);
        }
        
        [HttpPost("postSlucajaSaAdvokatimaSaCenovnikom")]
        public IActionResult postSlucajaSaAdvokatimaSaCenovnikom([FromBody] postSlucajAdvokataSaCenovnikomViewModel slucajVM)
        {
            if (!ModelState.IsValid)
            {  
                return BadRequest(ModelState);
            }
            foreach (var item in slucajVM.Advokats)
            {
                if (_context.SlucajAdvokats.Where(s => s.AdvokatId == item.Id && s.SlucajId == slucajVM.Slucaj.Id) != null)
                {
                    HttpResponseMessage mes;
                    string mess = "Vec ste dodali za svoj slucaj advokata: " + item.Ime + " " + item.Prezime ;
                    // naci kako vratiti poruku i napraviti svoj status kod ako moze
                    return BadRequest(mess);
                    //return StatusCode(405, mess);
                }               
            }
            
            try
            {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);
            _context.SlucajAdvokats.Include(q => q.Slucaj.Korisnik == ulogovaniKorisnik);
           
            slucajVM.Korisnik = ulogovaniKorisnik;
            slucajVM.KorisnikId = ulogovaniKorisnik.Id;
            slucajVM.Slucaj.Cenovniks = slucajVM.Cenovniks;
            slucajVM.Slucaj.Korisnik = slucajVM.Korisnik;
            
                foreach (Advokat advokat in slucajVM.Advokats)
                {
                    var newSlucajAdvokat = new SlucajAdvokat
                    {
                        AdvokatId = advokat.Id,
                        datumKreiranja = DateTime.UtcNow,
                        SlucajId = slucajVM.Slucaj.Id,
                        SlucajStatusId = 1
                    };
                    _context.SlucajAdvokats.Add(newSlucajAdvokat);
                    
                }
                foreach (var item in slucajVM.Cenovniks)
                {
                    var newCenovnik = new Cenovnik
                    {
                        vrstaPlacanja = item.vrstaPlacanja,
                        kolicina = item.kolicina,
                        StatusId = 1,
                        SlucajId = slucajVM.Slucaj.Id,
                        IdenityId = cliems.Value
                    };
                    _context.Cenovniks.Add(newCenovnik);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
     
            return Ok();
        }
        [HttpPost("postSlucajAdvokatima")]
        public async Task<IActionResult> PostSlucajAdvokatima([FromBody] postSlucajViewModel slucajVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var x = slucajVm;
            return Ok();
        }
        [HttpGet("getSlucajNaCekanju")]
        public IEnumerable<SlucajAdvokat> getSlucajNaCekanju()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Korisniks.Single(k => k.Idenity.Id == cliems.Value);

            var korsinikSlucajevi = _context.Slucajs.Where(s => s.Korisnik == ulogovaniKorisnik).Select(i => i.Id);
            var sviSlucajiKorisnika = _context.SlucajAdvokats.Where(d => d.SlucajStatusId == 1).Include(a => a.Advokat).Include(s => s.Slucaj).ThenInclude(c => c.Cenovniks);

            return sviSlucajiKorisnika;
        }
        // VEROVATNO NE TREBA PROVERITI
        //[HttpGet("getSlucajiPrihvaceniKorisnik")]
        //public IEnumerable<SlucajAdvokat> getSlucajiPrihvaceni()
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniKorisnik = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);
            
        //    var korsinikSlucajevi = _context.Slucajs.Where(s => s.Korisnik == ulogovaniKorisnik).Select(i => i.Id);
        //    var sviSlucajiKorisnika = _context.SlucajAdvokats.Where(d => d.SlucajStatusId == 2).Include(a => a.Advokat).Include(s => s.Slucaj).ThenInclude(c => c.Cenovniks);
            
        //    return sviSlucajiKorisnika;
        //}
        [HttpPut("prihvacenSlucajKorisnik")]
        public async Task<IActionResult> prihvacenSlucajKorisnik([FromBody] SlucajAdvokat slucajAdvokat)
        {

            slucajAdvokat.SlucajStatusId = 4;
            _context.Entry(slucajAdvokat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(slucajAdvokat);
        }
        //  VEROVATNO NE TREBA PROVERITI!!!
        //[HttpGet("getSlucajNaCekanjuKorisnik")]
        //public IEnumerable<SlucajAdvokat> getSlucajNaCekanjuKorisnik()
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniKorisnik = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);

        //    var sviSlucajiKorisnika = _context.SlucajAdvokats.Where(d => d.SlucajStatusId ==3).Include(a => a.Advokat).Include(s => s.Slucaj).ThenInclude(c => c.Cenovniks);
                       
        //    return sviSlucajiKorisnika;
        //}
        [HttpPut("odbijenSlucajOdKorisnika")]
        public async Task<IActionResult> odbijenSlucajOdKorisnika([FromBody] SlucajAdvokat slucajAdvokat)
        {

            slucajAdvokat.SlucajStatusId = 5;
            _context.Entry(slucajAdvokat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}