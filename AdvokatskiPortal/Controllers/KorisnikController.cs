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
        // KORISTI SE
        [HttpGet("getAllSlucajForKorisnik")]
        public IEnumerable<Slucaj> getAllSlucajForKorisnik()
        {
            try
            {
                var x = User.Claims.FirstOrDefault().Value;
                var korsinikSlucajevi = _context.Slucajs.Where(k => k.Korisnik.Idenity.Id == x).Include(sm => sm.SlucajMajstors).Include(k => k.Kategorija).Include(s =>s.Slike);

                var noviSlucajevi = korsinikSlucajevi.Where(y => y.SlucajMajstors == null);

                return korsinikSlucajevi;
            }
            catch (Exception e)
            {

                throw;
            }

        }
        [HttpGet("getSlucajById/{id}")]
        public Slucaj GetSlucajById([FromRoute]int id)
        {
            return _context.Slucajs.Where(s => s.Id == id).Include(sl => sl.Slike).Single();
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
                        cenovnici = HandleCenovnik(t.Slucaj.Cenovniks.Single(s => s.MajstorId == t.MajstorId && s.SlucajId == t.Slucaj.Id)),
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
                            Slike = t.Slucaj.Slike.Select(slika => new { slika.slikaProp }),
                            t.datumKreiranja
                        }
                    };
                }).ToList();
           
            return Ok(result);
        }
        private object HandleCenovnik(Cenovnik cenovnik)
        {
            if (cenovnik == null)
                return null;

            return new
            {
                cenovnik.kolicina,
                cenovnik.zavrsetakRada,
                cenovnik.PocetakRada,
                cenovnik.vrstaPlacanja,
                cenovnik.isKonacan,
                cenovnik.PrimanjeSlucaja,
                cenovnik.IzmenaSlucaja
            };
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
        public IActionResult getNewNostifiationSlucaj()
        {
            try
            {
                var cliems = User.Claims.FirstOrDefault();
                var ulogovaniKorisnik = _context.Korisniks.Where(x => x.Idenity.Id == cliems.Value).Include(i => i.Idenity).Single();
                //var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Slucaj.Korisnik.Id == ulogovaniKorisnik.Id && s.isRead == false).Include(m => m.Majstor).Include(s => s.Slucaj).ToList();

                var notification = _context.Notifications.Where(n => n.UserId == ulogovaniKorisnik.Idenity.Id && n.isRead == false);

                foreach (var item in notification)
                {
                    item.isRead = true;
                    _context.Entry(item).State = EntityState.Modified;
                }
                _context.SaveChanges();
                return Ok(notification);

            }
            catch (Exception e)
            {

                throw;
            }
        }
        //[HttpPut("putNewNostifiationReadKorisnik")]
        //public IActionResult putNewNostifiationReadKorisnik([FromBody] List<SlucajMajstor> nostification)
        //{
        //    var cliems = User.Claims.FirstOrDefault();
        //    var ulogovaniKorisnik = _context.Korisniks.SingleOrDefault(x => x.Idenity.Id == cliems.Value);

        //    var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Slucaj.Korisnik.Id == ulogovaniKorisnik.Id && s.isRead == false).Include(m => m.Majstor).Include(s => s.Slucaj).ToList();

        //    List<string> nost = noviSlucajevi.Select(s => "dobili ste novi slucaj " + s.Slucaj.Opis + " od " + s.Majstor.Ime + " " + s.Majstor.Prezime).ToList();
        //    var odbijeniSlucajevi = _context.SlucajMajstors.Where(os => os.Slucaj.Korisnik.Id == ulogovaniKorisnik.Id && os.isReadOdbijenAdvokat == false);
        //    nost = odbijeniSlucajevi.Select(o => o.Majstor.Ime + " " + o.Majstor.Prezime + " vas je odbio za slucaj " + o.Slucaj.Opis).ToList();



        //    return Ok(nost);
        //    //await _context.SaveChangesAsync();
            
        //}
       
        //[HttpPut("putNewNostifiationRead")]
        //public async Task<IActionResult> putNewNostifiationRead([FromBody] SlucajMajstor nostification)
        //{
        //    var cliems = User.Claims.FirstOrDefault();
        //    var ulogovaniKorisnik = _context.Korisniks.SingleOrDefault(x => x.Idenity.Id == cliems.Value);
        //    var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Majstor.Id == ulogovaniKorisnik.Id && s.isRead == false );
        //    foreach (var item in noviSlucajevi)
        //    {
        //        item.isRead = true;
        //        _context.Entry(item).State = EntityState.Modified;
        //    }

        //    await _context.SaveChangesAsync();
        //    return Ok(nostification);
        //}
        // KORISTI SE
        [HttpGet("getAllMajstori")]
        public IActionResult getAllMajstori()
        {

            try
            {
                var x = _context.Majstors.Include(k => k.Kategorije).ThenInclude(ka=>ka.Kategorija).Include(i => i.Idenity);
                return Ok(x);
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
        // KORISTI SE
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
        //  KORISTI SE  
        [HttpPost("postSlucajMajstorima")]
        public IActionResult postSlucajMajstorima([FromBody] postSlucajAdvokataSaCenovnikomViewModel slucajVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in slucajVM.Majstors)
            {

                if (_context.SlucajMajstors.Where(s => s.MajstorId == item.Id && s.SlucajId == slucajVM.Slucaj.Id).FirstOrDefault() != null)
                {
                    string mess = "Vec ste dodali za svoj slucaj majstorima: " + item.Ime + " " + item.Prezime;

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
                        datumKreiranja = DateTime.UtcNow.ToLocalTime(),
                        SlucajId = slucajVM.Slucaj.Id,
                        SlucajStatusId = 1
                    };
                    var notification = new Notification
                    {
                        UserId = newSlucajAdvokat.MajstorIdStr,
                        TimeStamp = DateTime.UtcNow.ToLocalTime(),
                        isRead = false,
                        SlucajId = slucajVM.Slucaj.Id,
                        NotificationText = $"{ulogovaniKorisnik.Ime} vam je dodao slucaj:  {slucajVM.Slucaj.Naziv}"
                    };
                    var cenovnik = new Cenovnik
                    {
                        MajstorId = majstor.Id,
                        StatusId = 1,
                        SlucajId = slucajVM.Slucaj.Id,
                        PrimanjeSlucaja = DateTime.UtcNow.ToLocalTime()
                        //zavrsetakRada = null
                    };
                    _context.SlucajMajstors.Add(newSlucajAdvokat);
                    _context.Cenovniks.Add(cenovnik);
                    _context.Notifications.Add(notification);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Ok();
        }
        //KORISTI SE
        [HttpPut("postavljanjeNoveCeneOdKorisnika")]
        public async Task<IActionResult> postavljanjeNoveCeneOdKorisnika([FromBody] Cenovnik noviCenovnikVM)
        {

            try
            {
                var noviCenovnik = _context.Cenovniks.Single(c => c.MajstorId == noviCenovnikVM.MajstorId && c.SlucajId == noviCenovnikVM.Slucaj.Id);
                var cliems = User.Claims.First();
                var ulogovaniKorisnik = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);
                noviCenovnik.kolicina = noviCenovnikVM.kolicina;
                noviCenovnik.vrstaPlacanja= noviCenovnikVM.vrstaPlacanja;
                noviCenovnik.komentar = noviCenovnikVM.komentar;
                noviCenovnik.IzmenaSlucaja = DateTime.UtcNow.ToLocalTime();
                _context.Entry(noviCenovnik).State = EntityState.Modified;
                var slucaj = _context.SlucajMajstors.Where(x => x.MajstorId == noviCenovnikVM.MajstorId && x.SlucajId == noviCenovnikVM.Slucaj.Id)
                                                          .Include(m =>m.Majstor).ThenInclude(i=>i.Idenity).Include(s => s.Slucaj).ThenInclude(k=>k.Korisnik).Single();
                var notification = new Notification
                {
                    UserId = slucaj.Majstor.Idenity.Id,
                    TimeStamp = DateTime.UtcNow.ToLocalTime(),
                    isRead = false,
                    SlucajId = slucaj.SlucajId,
                    NotificationText = $"{slucaj.Slucaj.Korisnik.Ime} je prepravio slucaj:  {slucaj.Slucaj.Naziv}"
                };
                slucaj.SlucajStatusId = 6;
                _context.Entry(slucaj).State = EntityState.Modified;
                _context.Notifications.Add(notification);

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
        //KOTISTI SE
        [HttpPut("editSlucaj")]
        public IActionResult editSlucaj([FromBody] Slucaj slucaj)
        {
            var currentSlucaj = _context.Slucajs.Where(s => s.Id == slucaj.Id).Include(sli => sli.Slike).Single();
            
            
            var ids = slucaj.Slike.Select(x => x.Id);
            var diff = currentSlucaj.Slike.Select(q => q.Id).Except(ids);
            List<Slika> slikeRemove = new List<Slika>();
            foreach (var item in diff)
            {
                slikeRemove.AddRange(_context.Slikas.Where(s => s.Id == item));

            }
            _context.Slikas.RemoveRange(slikeRemove);
            _context.SaveChanges();

            foreach (var slika in slucaj.Slike)
            {
                if(slika.Id == 0)
                {
                    var novaSlika = new Slika()
                     {
                        Naziv = slika.Naziv,
                        slikaProp = slika.slikaProp,
                        SlucajId = slucaj.Id
                    };
                    _context.Slikas.Add(novaSlika);               
                 }
            }
            _context.Entry(currentSlucaj).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(currentSlucaj);
        }
        [HttpPut("prihvacenSlucajKorisnik")]
        public async Task<IActionResult> prihvacenSlucajKorisnik([FromBody] acceptVM ids)
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Korisniks.Single(k => k.Idenity.Id == cliems.Value);

            //slucajMajstor.SlucajStatusId = 4;
            var slucaj = _context.Slucajs.SingleOrDefault(c => c.Id == ids.slucajId)/*.StatusId = 2*/;
            var slucajMajstor = _context.SlucajMajstors.Where(sl => sl.Slucaj.KorisnikId == ulogovaniKorisnik.Id 
                                                  && sl.SlucajId == ids.slucajId);
            List<int> odbijeniAdvokatiId = new List<int>();
            foreach (var item in slucajMajstor)
            {
                if (item.MajstorId == ids.majstorId)
                {
                    item.SlucajStatusId = 2;
                }
                else
                {
                    item.SlucajStatusId = 3;
                    item.isReject = true;
                }
                _context.Entry(item).State = EntityState.Modified;
            }
            var majstor = _context.Majstors.Where(x => x.Id == ids.majstorId).Include(i => i.Idenity).Single();
            var notification = new Notification
            {
                UserId = majstor.Idenity.Id,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                SlucajId = slucaj.Id,
                NotificationText = $"{ulogovaniKorisnik.Ime} je prihvatio slucaj:  {slucaj.Naziv}"
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("odbijenSlucajOdKorisnika")]
        public async Task<IActionResult> odbijenSlucajOdKorisnika([FromBody] acceptVM odbijenSlucaj)
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Korisniks.Single(k => k.Idenity.Id == cliems.Value);

            var slucaj = _context.SlucajMajstors.Where(x => x.MajstorId == odbijenSlucaj.majstorId&& x.Slucaj.Id == odbijenSlucaj.slucajId).Include(s=>s.Slucaj).Single();
            var notification = new Notification
            {
                UserId = slucaj.MajstorIdStr,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                SlucajId = odbijenSlucaj.slucajId,
                NotificationText = $"{ulogovaniKorisnik.Ime} je odbio slucaj:  {slucaj.Slucaj.Naziv}"
            };
            _context.Notifications.Add(notification);
            slucaj.SlucajStatusId = 3;
            _context.Entry(slucaj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}