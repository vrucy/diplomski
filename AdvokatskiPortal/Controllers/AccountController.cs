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

namespace AdvokatskiPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost("registrationAdvokat")]
        public async Task<IActionResult> RegistarAdvokat([FromBody] Advokat advokat)
        {

            var appUser = new ApplicationUser { UserName = advokat.UserName, Email = advokat.Email };

            var result = await userManager.CreateAsync(appUser, advokat.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await signInManager.SignInAsync(appUser, isPersistent: false);
            advokat.Idenity = appUser;

            var user = await userManager.FindByNameAsync(advokat.UserName);
            
            await userManager.AddClaimAsync(appUser, new Claim("RegularAdvokat", appUser.Id));
            //await userManager.AddClaimAsync(appUser, new Claim("AdminAdvokat", appUser.Id));
            

            _context.Advokats.Add(advokat);

            await _context.SaveChangesAsync();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(signingCredentials: signinCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            var claim = await userManager.GetClaimsAsync(user);

            string i = claim.First().Type;

            return Ok(new { Token = token, typeOfClaim = i });
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

            return Ok(new { Token = token, typeOfClaim = i });
        }
        [HttpPost("postRequestAdvokats")]
        public async Task<IActionResult> PostRequestAdvokats([FromBody]LoginModel loginUser)
        {

            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel loginUser)
        {
            var result = await signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, false);
            if (!result.Succeeded)
            {
                return BadRequest();
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

            string i = claim.First().Type;

            var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Ok(new { Token = token, typeOfClaim = i });
        }
    }
}