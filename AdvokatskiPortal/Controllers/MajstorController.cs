using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MajstorskiPortal.Data;
using MajstorskiPortal.Models;
using MajstorskiPortal.Models.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MajstorskiPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MajstorController : ControllerBase
    {
        private readonly PortalMajstoraDbContext _context;
        readonly UserManager<IdentityUser> userManager;
        readonly SignInManager<IdentityUser> signInManager;
        public MajstorController(PortalMajstoraDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet("getAllMajstori")]
        public IEnumerable<Majstor> getAllMajstori()
        {
            var x = _context.Majstors.Include(i => i.Idenity);
            return x;
        }

        [HttpGet("getNewNostifiation")]
        public IActionResult getNewNostifiationSlucaj()
        {
            try
            {
                //   potrebno je setovati za specificnog majstora slicno i za ma
                var cliems = User.Claims.FirstOrDefault();
                var ulogovaniKorisnik = _context.Users.Where(x => x.Id == cliems.Value).Single();

                var notification = _context.Notifications.Where(n => n.UserId == ulogovaniKorisnik.Id && n.isRead == false)
                                                               .Include(s => s.Slucaj).ThenInclude(sl => sl.Slike).ToList();
                //var result = notification.Select(n => new
                //{
                //    n.Slucaj.Naziv,
                //    n.Slucaj.Opis,
                //    n.Slucaj.Slike,
                //    n.isRead
                //});
                
                foreach (var item in notification)
                {
                    item.isRead = true;
                    _context.Entry(item).State = EntityState.Modified;
                }
                _context.SaveChanges();

                return Ok(notification);
            }
            catch
            {
                return BadRequest();
            }
            // potrebno prebaciti isRead na true;
        }

        //[HttpPut("putNewNostifiationReadMajstor")]
        //public async Task<IActionResult> putNewNostifiationReadMajstor([FromBody] SlucajMajstor nostification)
        //{
        //    var cliems = User.Claims.FirstOrDefault();
        //    var ulogovaniKorisnik = _context.Korisniks.SingleOrDefault(x => x.Idenity.Id == cliems.Value);
        //    var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Majstor.Id == ulogovaniKorisnik.Id && s.isReadOdbijenKorisnik == false);
        //    foreach (var item in noviSlucajevi)
        //    {
        //        item.isReadOdbijenKorisnik = true;
        //        _context.Entry(item).State = EntityState.Modified;
        //    }

        //    await _context.SaveChangesAsync();
        //    return Ok(nostification);
        //}
        //[HttpPut("putNewNostifiationRead")]
        //public async Task<IActionResult> putNewNostifiationRead([FromBody] SlucajMajstor[] nostification)
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);
        //    // potrebno prebaciti isRead na true;
        //    var noviSlucajevi = _context.SlucajMajstors.Where(s => s.Majstor.Id == ulogovaniKorisnik.Id && s.isRead == false).Include(s => s.Slucaj);
        //    foreach (var item in noviSlucajevi)
        //    {
        //        item.isRead = true;
        //        _context.Entry(item).State = EntityState.Modified;
        //    };

        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
        [HttpGet("getUgovorsForMajstor")]
        public IActionResult getUgovorsForMajstorAsync()
        {
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);

            var cenovnici = _context.Cenovniks.Include(c => c.Slucaj.SlucajMajstors).Include(c => c.Slucaj.Korisnik).Include(c => c.Majstor).Where(c => c.MajstorId == ulogovaniKorisnik.Id);

            var result = cenovnici.Select(c => new
            {
                c.Id,
                c.Slucaj.Korisnik.Ime,
                c.Slucaj.Korisnik.Prezime,
                Slucaj = new
                {
                    c.Slucaj.Id,
                    c.Slucaj.Slike,
                    c.Slucaj.Opis,
                    c.Slucaj.Naziv
                },
                c.Slucaj.KrajnjiRokZaOdgovor,
                c.zavrsetakRada,
                c.PocetakRada,
                c.IzmenaSlucaja,
                c.PrimanjeSlucaja,
                Kolicina = c.kolicina,
                VrstaPlacanja = c.vrstaPlacanja,
                SlucajStatusId = c.Slucaj.SlucajMajstors.First(sm => sm.SlucajId == c.Slucaj.Id && sm.MajstorId == c.Majstor.Id).SlucajStatusId,
                majstorId = c.MajstorId

            });


            return Ok(result);
        }
        //[HttpGet("getSlucajNaCekanju")]
        //public IEnumerable<SlucajMajstor> getSlucajNaCekanju()
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);

        //    var sviSlucajiMajstora = _context.SlucajMajstors.Where(a => a.Majstor.Id == ulogovaniKorisnik.Id).Include(t => t.Slucaj.Cenovniks).Include(s => s.Slucaj).ThenInclude(c => c.Korisnik).Where(q => q.SlucajStatusId == 1);

        //    return sviSlucajiMajstora;
        //}

        //[HttpGet("getSlucajiPrihvaceni")]
        //public IEnumerable<SlucajMajstor> getSlucajiPrihvaceni()
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);

        //    var sviSlucajiMajstora = _context.SlucajMajstors.Where(a => a.Majstor.Id == ulogovaniKorisnik.Id).Include(t => t.Slucaj.Cenovniks).Include(s => s.Slucaj).ThenInclude(c => c.Korisnik).Where(q => q.SlucajStatusId == 4);

        //    return sviSlucajiMajstora;
        //}

        /// <summary>
        /// odobrenje odbijanje i prihvatanje slucaja
        /// </summary>
        /// <param name="slucajMajstor"></param>
        /// <returns></returns>
        /// POTREBNO JE NAPRAVITI U CLIJENTU 2 POZIVA NA KLIKOM EDIT 1 DA SE NAPRAVI NOVI CENOVNIK A DRUGI DA SE EDITUJE STANJE TOG SLUCAJA NA ODGOVOR ADVOKAT
        [HttpPost("postavljanjeNoveCeneOdMajstora")]
        public async Task<IActionResult> postavljanjeNoveCeneOdMajstora([FromBody] Cenovnik noviCenovnikVM)
        {

            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);

            var cenovnik = new Cenovnik
            {
                //IdenityId = cliems.Value,
                SlucajId = noviCenovnikVM.SlucajId,
                kolicina = noviCenovnikVM.kolicina,
                komentar = noviCenovnikVM.komentar,
                vrstaPlacanja = noviCenovnikVM.vrstaPlacanja,
                StatusId = 1
            };
            _context.Cenovniks.Add(cenovnik);

            await _context.SaveChangesAsync();

            return Ok(cenovnik);

        }
        //KORISNTI SE
        [HttpPut("prepravkaCeneOdMajstora")]
        public async Task<IActionResult> prepravkaCeneOdMajstora([FromBody] Cenovnik noviCenovnikVM)
        {
            //editCenovnikMajstorVM
            //await _context.SaveChangesAsync();
            try
            {
                var cliems = User.Claims.First();
                //noviCenovnikVM.IdenityId = cliems.Value;
                noviCenovnikVM.SlucajId = noviCenovnikVM.Slucaj.Id;
                noviCenovnikVM.StatusId = 1;
                //var slucaj = _context.Slucajs.Single(x => x.Id == noviCenovnikVM.Slucaj.Id);
                //slucaj.zavrsetakRada = noviCenovnikVM.Slucaj.zavrsetakRada;
                //_context.Entry(slucaj).State = EntityState.Modified;

                _context.Entry(noviCenovnikVM).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }

            return Ok();

        }
        [HttpPut("prihvatanjeSlucajaMajstora")]
        public async Task<IActionResult> prihvatanjeSlucajaMajstor([FromBody] acceptVM ids)
        {
                var cliems = User.Claims.First();
                var ulogovaniKorisnik = _context.Majstors.Single(k => k.Idenity.Id == cliems.Value);
                var slucaj = _context.SlucajMajstors.Where(sl => sl.MajstorId == ulogovaniKorisnik.Id && sl.SlucajId == ids.slucajId)
                                                          .Include(i => i.Slucaj.Korisnik.Idenity).Single();

                var notification = new Notification
                {
                    UserId = slucaj.Slucaj.Korisnik.Idenity.Id,
                    TimeStamp = DateTime.UtcNow.ToLocalTime(),
                    isRead = false,
                    SlucajId = slucaj.SlucajId,
                    NotificationText = $"{slucaj.Majstor.Ime} je prihvatio da radi na slucaju:  {slucaj.Slucaj.Naziv}"
                };
                _context.Notifications.Add(notification);
                slucaj.SlucajStatusId = 4;
                _context.Entry(slucaj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
           
        }
        //[HttpPut("prihvatanjeSlucajaMajstora")]
        //public async Task<IActionResult> prihvatanjeSlucajaMajstora([FromBody] SlucajMajstor slucajMajstor)
        //{
        //    if (slucajMajstor.SlucajStatusId == 1 || slucajMajstor.SlucajStatusId == 3 || slucajMajstor.SlucajStatusId == 6)
        //    {
        //        var cliems = User.Claims.First();
        //        var ulogovaniKorisnik = _context.Majstors.Single(k => k.Idenity.Id == cliems.Value);
        //        var slucaj = _context.SlucajMajstors.Where(sl => sl.MajstorId == ulogovaniKorisnik.Id && sl.SlucajId == slucajMajstor.Slucaj.Id)
        //                                                  .Include(i => i.Slucaj.Korisnik.Idenity).Single();
            
        //        var notification = new Notification
        //        {
        //            UserId = slucaj.Slucaj.Korisnik.Idenity.Id,
        //            TimeStamp = DateTime.UtcNow.ToLocalTime(),
        //            isRead = false,
        //            SlucajId = slucaj.SlucajId,
        //            NotificationText = $"{slucaj.Majstor.Ime} je prihvatio da radi na slucaju:  {slucaj.Slucaj.Naziv}"
        //        };
        //        _context.Notifications.Add(notification);
        //        slucaj.SlucajStatusId = 4;
        //        _context.Entry(slucaj).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
        // KORISTI SE
        [HttpPut("prepravkaSlucajaMajstora")]
        public async Task<IActionResult> prepravkaSlucajaMajstora([FromBody] Cenovnik slucajMajstor)
        {
            //slucajMajstor.Slucaj.ZavrsetakRada = slucajMajstor.Slucaj.ZavrsetakRada;
            var cliems = User.Claims.First();
            var ulogovaniKorisnik = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);
            var slucaj = _context.SlucajMajstors.Where(x => x.MajstorId == slucajMajstor.MajstorId && x.SlucajId == slucajMajstor.Slucaj.Id)
                                                        .Include(m => m.Majstor).Include(s => s.Slucaj).ThenInclude(k => k.Korisnik).ThenInclude(i => i.Idenity).Single();
            var noviCenovnik = _context.Cenovniks.Single(c => c.MajstorId == slucajMajstor.MajstorId && c.SlucajId == slucajMajstor.Slucaj.Id);
            noviCenovnik.kolicina = slucajMajstor.kolicina;
            noviCenovnik.vrstaPlacanja = slucajMajstor.vrstaPlacanja;
            noviCenovnik.komentar = slucajMajstor.komentar;
            noviCenovnik.PocetakRada = slucajMajstor.PocetakRada;
            noviCenovnik.zavrsetakRada = slucajMajstor.zavrsetakRada;
            noviCenovnik.isKonacan = slucajMajstor.isKonacan;
            noviCenovnik.IzmenaSlucaja = DateTime.UtcNow.ToLocalTime();
            _context.Entry(noviCenovnik).State = EntityState.Modified;

            var notification = new Notification
            {
                UserId = slucaj.Slucaj.Korisnik.Idenity.Id,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                SlucajId = slucaj.SlucajId,
                NotificationText = $"{slucaj.Majstor.Ime} je prepravio slucaj:  {slucaj.Slucaj.Naziv}"
            };
            _context.Notifications.Add(notification);

            slucaj.SlucajStatusId = 7;
            _context.Entry(slucaj).State = EntityState.Modified;
            //_context.Entry(slucajMajstor.Slucaj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("odbijanjeSlucajaMajstora")]
        public async Task<IActionResult> odbijanjeSlucajaMajstora([FromBody] acceptVM slucajMajstor)
        {
            var slucaj = _context.SlucajMajstors.Where(x => x.MajstorId == slucajMajstor.majstorId && x.Slucaj.Id == slucajMajstor.slucajId)
                                                      .Include(m => m.Majstor).Include(s => s.Slucaj).ThenInclude(k => k.Korisnik).ThenInclude(i => i.Idenity).Single();
            var notification = new Notification
            {
                UserId = slucaj.Slucaj.Korisnik.Idenity.Id,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                SlucajId = slucaj.SlucajId,
                NotificationText = $"{slucaj.Majstor.Ime} je odbio slucaj:  {slucaj.Slucaj.Naziv}"
            };
            _context.Notifications.Add(notification);
            slucaj.SlucajStatusId = 5;
            _context.Entry(slucaj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
        [Authorize(Policy = "AdminMajstor")]
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