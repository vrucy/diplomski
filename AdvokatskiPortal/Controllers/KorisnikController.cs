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
        public IActionResult getAllSlucajAdvokatForKorisnik()
        {
            var x = User.Claims.FirstOrDefault().Value;
            var korsinikSlucajevi = _context.Slucajs.Where(k => k.Korisnik.Idenity.Id == x).Include(c => c.Cenovniks).Include(k => k.Korisnik).ThenInclude(i => i.Idenity).ToList();
            
            var test = korsinikSlucajevi.SelectMany(slucaj => _context.SlucajMajstors.Where(d => d.SlucajId == slucaj.Id).Include(a => a.Majstor.Idenity).Include(s => s.Slucaj)
                                        .ThenInclude(sl => sl.Slike));
            

                var result = test.Select(t =>
                {
                    return new
                    {
                        t.Majstor.Ime,
                        t.Majstor.Prezime,
                        MajstorId = t.MajstorId,
                        cenovnici = t.Slucaj.Cenovniks.Single(s => s.MajstorId == t.MajstorId && s.SlucajId == t.Slucaj.Id)
                        != null ? t.Slucaj.Cenovniks.Single(s => s.MajstorId == t.MajstorId && s.SlucajId == t.Slucaj.Id) : null,
                        //vrstaPlacanja = t.Slucaj.Cenovniks != null ? t.Slucaj.Cenovniks.FirstOrDefault().vrstaPlacanja : null,
                        //kolicina = t.Slucaj.Cenovniks != null ? t.Slucaj.Cenovniks.FirstOrDefault().kolicina : null,
                        //idMajstoraCenovnik = t.Slucaj.Cenovniks != null ? t.Slucaj.Cenovniks.FirstOrDefault().IdenityId : null,
                        //cenovnikId = t.Slucaj.Cenovniks.FirstOrDefault().Id,
                        //t.Slucaj.Cenovniks.FirstOrDefault().StatusId,
                        // TREBA UBACITI ZAVRSETAK RADA
                        //t.ZavrsetakRada,
                        t.SlucajStatusId,
                        idMajstor = t.Majstor.Idenity.Id,
                        Slucaj = new
                        {
                            Id = t.SlucajId,
                            t.Slucaj.Naziv,
                            t.Slucaj.Opis,
                            t.Slucaj.Slike
                        }
                    };
                });
           
            return Ok(result);
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
        public IEnumerable<string> getNewNostifiationSlucaj()
        {
            try
            {
                var cliems = User.Claims.FirstOrDefault();
                var ulogovaniKorisnik = _context.Korisniks.SingleOrDefault(x => x.Idenity.Id == cliems.Value);
                var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Slucaj.Korisnik.Id == ulogovaniKorisnik.Id && s.isRead == false).Include(m => m.Majstor).Include(s => s.Slucaj).ToList();

                List<string> nost = noviSlucajevi.Select(s => "dobili ste novi slucaj " + s.Slucaj.Opis + " od " + s.Majstor.Ime + " " + s.Majstor.Prezime).ToList();
                var odbijeniSlucajevi = _context.SlucajMajstors.Where(os => os.Slucaj.Korisnik.Id == ulogovaniKorisnik.Id && os.isReadOdbijenAdvokat == false);
                nost = odbijeniSlucajevi.Select(o => o.Majstor.Ime + " " + o.Majstor.Prezime + " vas je odbio za slucaj " + o.Slucaj.Opis).ToList();

                return nost;

            }
            catch (Exception e)
            {

                throw;
            }
            // potrebno prebaciti isRead na true;
        }
        //[HttpGet("getNewNostifiationOdbijeni")]
        //public IEnumerable<string> getNewNostifiation()
        //{
        //    List<string> nost = new List<string>();

        //    try
        //    {
        //        var cliems = User.Claims.FirstOrDefault();
        //        var ulogovaniKorisnik = _context.Korisniks.SingleOrDefault(x => x.Idenity.Id == cliems.Value);
        //        var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Slucaj.KorisnikId == ulogovaniKorisnik.Id && s.isReadOdbijenKorisnik == false).ToList();
        //        foreach (var item in noviSlucajevi)
        //        {
        //            if (item.SlucajStatusId == 5 )
        //            {
        //                nost.Add(item.Majstor.Ime + item.Majstor.Prezime + "vas je odbio za slucaj" + item.Slucaj.Opis);
        //            }

        //        }
        //        return nost;
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }
        //    // potrebno prebaciti isRead na true;
        //}
        [HttpPut("putNewNostifiationReadKorisnik")]
        public async Task<IActionResult> putNewNostifiationReadKorisnik([FromBody] SlucajMajstor nostification)
        {
            var cliems = User.Claims.FirstOrDefault();
            var ulogovaniKorisnik = _context.Korisniks.SingleOrDefault(x => x.Idenity.Id == cliems.Value);
            var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Majstor.Id == ulogovaniKorisnik.Id && s.isReadOdbijenKorisnik == false);
            foreach (var item in noviSlucajevi)
            {
                item.isReadOdbijenKorisnik = true;
                _context.Entry(item).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return Ok(nostification);
        }
        [HttpPut("putNewNostifiationRead")]
        public async Task<IActionResult> putNewNostifiationRead([FromBody] SlucajMajstor nostification)
        {
            var cliems = User.Claims.FirstOrDefault();
            var ulogovaniKorisnik = _context.Korisniks.SingleOrDefault(x => x.Idenity.Id == cliems.Value);
            var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Majstor.Id == ulogovaniKorisnik.Id && s.isRead == false );
            foreach (var item in noviSlucajevi)
            {
                item.isRead = true;
                _context.Entry(item).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return Ok(nostification);
        }
        [HttpGet("getAllMajstori")]
        public IActionResult getAllMajstori()
        {

            try
            {
                return Ok(_context.Majstors.Include(k => k.Kategorije).Include(i => i.Idenity));
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
                _context.SlucajMajstors.Include(m => m.Majstor.Idenity);
                


                slucajVM.Korisnik = ulogovaniKorisnik;
                slucajVM.KorisnikId = ulogovaniKorisnik.Id;
                // slucajVM.Slucaj.Cenovniks = slucajVM.Cenovniks;
                slucajVM.Slucaj.Korisnik = slucajVM.Korisnik;

                foreach (Majstor majstor in slucajVM.Majstors)
                {
                    var newSlucajAdvokat = new SlucajMajstor
                    {
                        MajstorId = majstor.Id,
                        MajstorIdStr = majstor.Idenity.Id,
                        datumKreiranja = DateTime.UtcNow,
                        SlucajId = slucajVM.Slucaj.Id,
                        SlucajStatusId = 1
                    };

                    var cenovnik = new Cenovnik
                    {
                        MajstorId = majstor.Id,
                        StatusId = 1,
                        SlucajId = slucajVM.Slucaj.Id
                    };
                    _context.SlucajMajstors.Add(newSlucajAdvokat);
                    _context.Cenovniks.Add(cenovnik);

                }
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

            try
            {
                var cliems = User.Claims.First();
                var ulogovaniKorisnik = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);
                noviCenovnikVM.IdenityId = cliems.Value;
                var slucaj = _context.SlucajMajstors.Single(x => x.MajstorId == noviCenovnikVM.MajstorId && x.SlucajId == noviCenovnikVM.Slucaj.Id);
                noviCenovnikVM.StatusId = 1;
                noviCenovnikVM.SlucajId = noviCenovnikVM.Slucaj.Id;
                _context.Entry(noviCenovnikVM).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }

            return Ok();

        }
        [HttpPut("prepravkaSlucajaKorisnik")]
        public async Task<IActionResult> prepravkaSlucajaKorisnik([FromBody] Cenovnik slucajMajstor)
        {

            try
            {
                // rucno nabajdovati slucaj Id slicno i gore treba
               // slucajMajstor.SlucajId = slucajMajstor.Slucaj.Id;
                var slucaj = _context.SlucajMajstors.Single(x => x.MajstorId == slucajMajstor.MajstorId && x.SlucajId == slucajMajstor.Slucaj.Id);
                slucaj.SlucajStatusId = 6;
                _context.Entry(slucaj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
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
        //[HttpGet("getSlucajNaCekanju")]
        //public IEnumerable<SlucajMajstor> getSlucajNaCekanju()
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniKorisnik = _context.Korisniks.Single(k => k.Idenity.Id == cliems.Value);

        //    var korsinikSlucajevi = _context.Slucajs.Where(s => s.Korisnik == ulogovaniKorisnik).Select(i => i.Id);
        //    var sviSlucajiKorisnika = _context.SlucajMajstors.Where(d => d.SlucajStatusId == 1).Include(a => a.Majstor).Include(s => s.Slucaj).ThenInclude(c => c.Cenovniks);

        //    return sviSlucajiKorisnika;
        //}

        [HttpPut("prihvacenSlucajKorisnik")]
        public async Task<IActionResult> prihvacenSlucajKorisnik([FromBody] acceptVM ids)
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Korisniks.Single(k => k.Idenity.Id == cliems.Value);

            //slucajMajstor.SlucajStatusId = 4;
            var slucaj = _context.Slucajs.SingleOrDefault(c => c.Id == ids.slucajId)/*.StatusId = 2*/;
            var x = _context.SlucajMajstors.Where(sl => sl.Slucaj.KorisnikId == ulogovaniKorisnik.Id 
                                                  && sl.SlucajId == ids.slucajId);
            List<int> odbijeniAdvokatiId = new List<int>();
            foreach (var item in x)
            {
                if (item.MajstorId == ids.majstorId)
                {
                    item.SlucajStatusId = 4;
                }
                else
                {
                    item.SlucajStatusId = 3;
                    // UBACITI U HELPER POZOVE METODU DA POSALJE OSTALIM ADVOKATIMA OBAVESTENJE DA SU ODBIJENI
                    odbijeniAdvokatiId.Add(item.MajstorId);
                    item.isReject = true;
                    // ubaciti bool da je odbijen u slucaj majstor
                }
                _context.Entry(item).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return Ok();
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