using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AdvokatskiPortal.Data;
using AdvokatskiPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using AdvokatskiPortal.Models.View;
using Microsoft.EntityFrameworkCore;

namespace AdvokatskiPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize("AdminAdvokat")]

    public class AccountController : ControllerBase
    {
        readonly UserManager<IdentityUser> userManager;
        readonly SignInManager<IdentityUser> signInManager;
        private readonly PortalAdvokataDbContext _context;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, PortalAdvokataDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._context = context;
        }
        //[Authorize( Policy = "AdminAdvokat")]
        [HttpPost("registrationAdvokat")]
        public async Task<IActionResult> RegistarAdvokat([FromBody] postMajstor majstor)
        {

            var newMajstor = new Majstor
            {
                Email = majstor.Email,
                Ime = majstor.Ime,
                Mesto = majstor.Mesto,
                Password = majstor.Password,
                Prezime = majstor.Prezime,
                Ulica = majstor.Ulica,
                UserName = majstor.UserName,
                //Kategorije = majstor.Kategorije.ToList()
            };
            _context.Majstors.Add(newMajstor);
            try
            {

                foreach (var item in majstor.Kategorije)
                {
                    var newMajtorKategorije = new MajstorKategorije
                    {
                        KategorijaId = item.Id,
                        MajstorId = newMajstor.Id,
                    };
                    _context.MajstorKategorijes.Add(newMajtorKategorije);
                }
            }
            catch (Exception e)
            {

                throw;
            }


            try
            {
                var appUser = new ApplicationUser { UserName = majstor.UserName, Email = majstor.Email };

                var result = await userManager.CreateAsync(appUser, majstor.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                await signInManager.SignInAsync(appUser, isPersistent: false);
                newMajstor.Idenity = appUser;

                var user = await userManager.FindByNameAsync(majstor.UserName);

                await userManager.AddClaimAsync(appUser, new Claim("RegularAdvokat", appUser.Id));
                await userManager.AddClaimAsync(appUser, new Claim("AdminAdvokat", appUser.Id));
                await _context.SaveChangesAsync();

                //var ids = majstor.Kategorije.Select(kat => kat.Id).ToList();
                //var dbReferences = _context.Kategorijas.Where(k => ids.Contains(k.Id)).ToList();
                //dbReferences.ForEach(r => newMajstor.Kategorije.Add(r));
                //newMajstor.Kategorije = majstor.Kategorije;
                //await _context.SaveChangesAsync();


                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(signingCredentials: signinCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                var claim = await userManager.GetClaimsAsync(user);

                string i = claim.First().Type;

                return Ok(new { Token = token, typeOfClaim = i, user = user.UserName });
            }
            catch (Exception e)
            {

                throw;
            }



        }
        [HttpPost("registration")]
        public async Task<IActionResult> Registar([FromBody] Korisnik korisnik)
        {

            var appUser = new ApplicationUser { UserName = korisnik.UserName, Email = korisnik.Email };

            var result = await userManager.CreateAsync(appUser, korisnik.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await signInManager.SignInAsync(appUser, isPersistent: false);
            korisnik.Idenity = appUser;

            var user = await userManager.FindByNameAsync(korisnik.UserName);

            await userManager.AddClaimAsync(appUser, new Claim("RegularUser", appUser.Id));

            _context.Korisniks.Add(korisnik);

            await _context.SaveChangesAsync();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(signingCredentials: signinCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            var claim = await userManager.GetClaimsAsync(user);

            string i = claim.First().Type;

            return Ok(new { Token = token, typeOfClaim = i, user = user.UserName });
        }
        [HttpGet("getKorisnik")]
        public IActionResult getKorisnik()
        {
            var cliems = User.Claims.First();
            var korisnik = _context.Korisniks.Single(x => x.Idenity.Id == cliems.Value);

            return Ok(korisnik);
        }
        [HttpGet("getMajstor")]
        public IActionResult getAdvokat()
        {
            var cliems = User.Claims.First();
            var majstor = _context.Majstors.Single(x => x.Idenity.Id == cliems.Value);

            return Ok(majstor);
        }
        [HttpGet("getCurrentUser/{userName}")]
        public async Task<IActionResult> getCurrentUser([FromRoute] string userName)
        {
            var korisnik = _context.Korisniks.Where(x => x.UserName == userName);
            var advokat = _context.Majstors.Where(x => x.UserName == userName);
            if (korisnik != null)
            {
                return Ok(korisnik);
            }
            else
            {
                return Ok(advokat);
            }


        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel loginUser)
        {
            string i;
            var result = await signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, false);
            if (!result.Succeeded)
            {
                return StatusCode(405);
            }
            var user = await userManager.FindByNameAsync(loginUser.UserName);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claim = await userManager.GetClaimsAsync(user);

            var tokeOptions = new JwtSecurityToken(
                claims: claim,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );
            if (claim.Where(c => c.Type == "AdminAdvokat").SingleOrDefault() != null)
            {
                i = claim.Where(c => c.Type == "AdminAdvokat").Single().Type;
            }
            else
            {
                i = claim.First().Type;
            }

            var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Ok(new { Token = token, typeOfClaim = i, user = user.UserName });
        }
        [HttpPut("editKorisnik")]
        public IActionResult editKorisnik(Korisnik korisnik)
        {
            var user = _context.Korisniks.Single(k => k.Id == korisnik.Id);
            user.Ime = korisnik.Ime;
            user.Prezime = korisnik.Prezime;
            user.UserName = korisnik.UserName;
            user.Password = korisnik.Password;
            user.Email = korisnik.Email;
            user.Mesto = korisnik.Mesto;
            user.Ulica = korisnik.Ulica;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("editMajstor")]
        public IActionResult editMajstor(Majstor majstor)
        {
            var user = _context.Majstors.Single(k => k.Id == majstor.Id);
            user.Ime = majstor.Ime;
            user.Prezime = majstor.Prezime;
            user.UserName = majstor.UserName;
            user.Password = majstor.Password;
            user.Email = majstor.Email;
            user.Mesto = majstor.Mesto;
            user.Ulica = majstor.Ulica;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }
    }
}