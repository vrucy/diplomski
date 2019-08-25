using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdvokatskiPortal.Data;
using AdvokatskiPortal.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AdvokatskiPortal.Models.View;

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
            //this._userManager = manager;
        }

        // GET: api/Korisnik
        [HttpGet]
        public IEnumerable<Korisnik> GetKorisniks()
        {
            return _context.Korisniks;
        }
        [HttpGet("getAllSlucajForKorisnik")]
        public IEnumerable<Slucaj> getAllSlucajForKorisnik()
        {
            var x = User.Claims.FirstOrDefault().Value;
            var korsinikSlucajevi = _context.Slucajs.Where(k => k.Korisnik.Idenity.Id == x);
            return korsinikSlucajevi;
        }
        // GET: api/Korisnik/5
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

        // PUT: api/Korisnik/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKorisnik([FromRoute] int id, [FromBody] Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != korisnik.Id)
            {
                return BadRequest();
            }

            _context.Entry(korisnik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KorisnikExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

        [HttpPost("PostSlucaj")]
        public async Task<IActionResult> PostSlucaj([FromBody] Slucaj slucaj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cliems = User.Claims.First();
            var y = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);
            slucaj.Korisnik = y ;
            slucaj.KorisnikId = y.Id;
            _context.Slucajs.Add(slucaj);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSlucaj", new { id = slucaj.Id }, slucaj);
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

        // DELETE: api/Korisnik/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKorisnik([FromRoute] int id)
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

            _context.Korisniks.Remove(korisnik);
            await _context.SaveChangesAsync();

            return Ok(korisnik);
        }

        private bool KorisnikExists(int id)
        {
            return _context.Korisniks.Any(e => e.Id == id);
        }
    }
}