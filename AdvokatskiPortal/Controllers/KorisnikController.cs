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
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json.Linq;

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
        public IEnumerable<SlucajMajstor> getAllSlucajAdvokatForKorisnik()
        {
            var x = User.Claims.FirstOrDefault().Value;
            var korsinikSlucajevi = _context.Slucajs.Where(k => k.Korisnik.Idenity.Id == x).Include(k => k.Korisnik).ThenInclude(i => i.Idenity).ToList();
            var test = new List<SlucajMajstor>();
            foreach (var item in korsinikSlucajevi)
            {
                test.AddRange(_context.SlucajMajstors.Where(d => d.SlucajId == item.Id).Include(a => a.Majstor.Idenity).Include(s => s.Slucaj).ThenInclude(c => c.Cenovniks));
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
        [HttpGet("getNewNostifiation")]
        public IEnumerable<SlucajMajstor> getNewNostifiation()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);
            // potrebno prebaciti isRead na true;
            var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Majstor.Id == ulogovaniKorisnik.Id && s.isRead == false);

            return noviSlucajevi;
        }
        [HttpPut("putNewNostifiationRead")]
        public async Task<IActionResult> putNewNostifiationRead([FromBody] SlucajMajstor nostification)
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);
            // potrebno prebaciti isRead na true;
            var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Majstor.Id == ulogovaniKorisnik.Id && s.isRead == false);

            return Ok(nostification);
        }
        [HttpGet("getAllMajstori")]
        public IActionResult getAllMajstori()
        {

            try
            {
                var x = _context.Majstors.Include(k => k.MajstorKategorijes);
                return Ok(_context.Majstors.Include(k => k.MajstorKategorijes));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetUgovorsForKorisnik")]
        public IEnumerable<SlucajMajstor> GetUgovorsForKorisnik()
        {

            var x = User.Claims.FirstOrDefault().Value;
            var korsinikSlucajevi = _context.Slucajs.Where(k => k.Korisnik.Idenity.Id == x).Select(i => i.Id);
            var f = _context.SlucajMajstors.Where(d => d.SlucajId == korsinikSlucajevi.FirstOrDefault()).Include(a => a.Majstor.Idenity).Include(s => s.Slucaj).ThenInclude(c => c.Cenovniks);

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
            try
            {
                var cliems = User.Claims.First();
                var q = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);
                slucaj.KorisnikId = q.Id;

                _context.Slucajs.Include(y => y.Korisnik.Id == q.Id);
                _context.Slucajs.Add(slucaj);


                await _context.SaveChangesAsync();
                return CreatedAtAction("GetSlucaj", new { id = slucaj.Id }, slucaj);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("getAllKategorije")]
        public IEnumerable<Kategorija> getAllKategorije()
        {
            return _context.Kategorijas;
        }
        [HttpPost("postSlucajaSaAdvokatimaSaCenovnikom")]
        public IActionResult postSlucajaSaAdvokatimaSaCenovnikom([FromBody] postSlucajAdvokataSaCenovnikomViewModel slucajVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in slucajVM.Majstors)
            {

                if (_context.SlucajMajstors.Where(s => s.MajstorId == item.Id && s.SlucajId == slucajVM.Slucaj.Id).FirstOrDefault() != null)
                {
                    string mess = "Vec ste dodali za svoj slucaj advokata: " + item.Ime + " " + item.Prezime;

                    return StatusCode(404, new { message = mess, customStatusCode = 999 });
                }
            }

            try
            {
                var cliems = User.Claims.First();
                var ulogovaniKorisnik = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);
                _context.SlucajMajstors.Include(q => q.Slucaj.Korisnik == ulogovaniKorisnik);

                slucajVM.Korisnik = ulogovaniKorisnik;
                slucajVM.KorisnikId = ulogovaniKorisnik.Id;
                // slucajVM.Slucaj.Cenovniks = slucajVM.Cenovniks;
                slucajVM.Slucaj.Korisnik = slucajVM.Korisnik;

                foreach (Majstor advokat in slucajVM.Majstors)
                {
                    var newSlucajAdvokat = new SlucajMajstor
                    {
                        MajstorId = advokat.Id,
                        datumKreiranja = DateTime.UtcNow,
                        SlucajId = slucajVM.Slucaj.Id,
                        SlucajStatusId = 1
                    };
                    _context.SlucajMajstors.Add(newSlucajAdvokat);

                }
                //foreach (var item in slucajVM.Cenovniks)
                //{
                //    var newCenovnik = new Cenovnik
                //    {
                //        vrstaPlacanja = item.vrstaPlacanja,
                //        kolicina = item.kolicina,
                //        StatusId = 1,
                //        SlucajId = slucajVM.Slucaj.Id,
                //        IdenityId = cliems.Value
                //    };
                //    _context.Cenovniks.Add(newCenovnik);
                //}
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Ok();
        }
        [HttpPut("postavljanjeNoveCeneOdKorisnika")]
        public async Task<IActionResult> postavljanjeNoveCeneOdKorisnika([FromBody] Cenovnik noviCenovnikVM)
        {

            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);
            noviCenovnikVM.IdenityId = ulogovaniKorisnik.Idenity.Id;
            _context.Entry(noviCenovnikVM).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
            
        }
        [HttpPut("prepravkaSlucajaKorisnik")]
        public async Task<IActionResult> prepravkaSlucajaKorisnik([FromBody] SlucajMajstor slucajMajstor)
        {

            slucajMajstor.SlucajStatusId = 7;
            _context.Entry(slucajMajstor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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
        public IEnumerable<SlucajMajstor> getSlucajNaCekanju()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Korisniks.Single(k => k.Idenity.Id == cliems.Value);

            var korsinikSlucajevi = _context.Slucajs.Where(s => s.Korisnik == ulogovaniKorisnik).Select(i => i.Id);
            var sviSlucajiKorisnika = _context.SlucajMajstors.Where(d => d.SlucajStatusId == 1).Include(a => a.Majstor).Include(s => s.Slucaj).ThenInclude(c => c.Cenovniks);

            return sviSlucajiKorisnika;
        }

        [HttpPut("prihvacenSlucajKorisnik")]
        public async Task<IActionResult> prihvacenSlucajKorisnik([FromBody] SlucajMajstor slucajMajstor)
        {

            slucajMajstor.SlucajStatusId = 4;
            _context.Entry(slucajMajstor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(slucajMajstor);
        }
        [HttpPut("odbijenSlucajOdKorisnika")]
        public async Task<IActionResult> odbijenSlucajOdKorisnika([FromBody] SlucajMajstor slucajMajstor)
        {

            slucajMajstor.SlucajStatusId = 5;
            _context.Entry(slucajMajstor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}