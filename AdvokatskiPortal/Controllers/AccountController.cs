using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CraftmanPortal.Data;
using CraftmanPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using CraftmanPortal.Models.View;
using Microsoft.EntityFrameworkCore;

namespace CraftmanPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize("AdminCraftman")]

    public class AccountController : ControllerBase
    {
        readonly UserManager<IdentityUser> userManager;
        readonly SignInManager<IdentityUser> signInManager;
        private readonly PortalOfCraftsmanDbContext _context;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, PortalOfCraftsmanDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._context = context;
        }
        //[Authorize(Policy = "AdminCraftman")]
        [HttpPost("RegistrationCraftman")]
        public async Task<IActionResult> RegistrationCraftman([FromBody] postCraftman Craftman)
        {

            var newCraftman = new Craftman
            {
                Email = Craftman.Email,
                FirstName = Craftman.FirstName,
                Place = Craftman.Place,
                Password = Craftman.Password,
                LastName = Craftman.LastName,
                Street = Craftman.Street,
                UserName = Craftman.UserName
            };
            _context.Craftmans.Add(newCraftman);
            
            foreach (var item in Craftman.Categories)
            {
                var newMajtorKategorije = new ContractCategory
                {
                    CategoryId = item.Id,
                    CraftmanId = newCraftman.Id,
                };
                _context.ContractCategores.Add(newMajtorKategorije);
            }
            var appUser = new ApplicationUser { UserName = Craftman.UserName, Email = Craftman.Email };

            var result = await userManager.CreateAsync(appUser, Craftman.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await signInManager.SignInAsync(appUser, isPersistent: false);
            newCraftman.Idenity = appUser;

            var user = await userManager.FindByNameAsync(Craftman.UserName);

            await userManager.AddClaimAsync(appUser, new Claim("RegularCraftman", appUser.Id));
            await userManager.AddClaimAsync(appUser, new Claim("AdminCraftman", appUser.Id));
            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] User User)
        {

            var appUser = new ApplicationUser { UserName = User.UserName, Email = User.Email };
            try
            {

            var result = await userManager.CreateAsync(appUser, User.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            }
            catch (Exception e)
            {

                throw;
            }


            await signInManager.SignInAsync(appUser, isPersistent: false);
            User.Idenity = appUser;

            var user = await userManager.FindByNameAsync(User.UserName);

            await userManager.AddClaimAsync(appUser, new Claim("RegularUser", appUser.Id));

            _context.Users.Add(User);

            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            var cliems = User.Claims.First();
            var loginUser = _context.Users.Single(x => x.Idenity.Id == cliems.Value);

            return Ok(loginUser);
        }
        [HttpGet("GetCraftman")]
        public IActionResult GetCraftman()
        {
            var cliems = User.Claims.First();
            var Craftman = _context.Craftmans.Single(x => x.Idenity.Id == cliems.Value);

            return Ok(Craftman);
        }
        [HttpGet("GetCurrentUser/{userName}")]
        public async Task<IActionResult> GetCurrentUser([FromRoute] string userName)
        {
            var User = _context.Users.Where(x => x.UserName == userName);
            var Craftman = _context.Craftmans.Where(x => x.UserName == userName);
            if (User != null)
            {
                return Ok(User);
            }
            else
            {
                return Ok(Craftman);
            }


        }

        [AllowAnonymous]
        [HttpPost("Login")]
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
            //TODO must refactor terible.
            if (claim.Where(c => c.Type == "AdminCraftman").SingleOrDefault() != null)
            {
                i = claim.Where(c => c.Type == "AdminCraftman").Single().Type;
            }
            else
            {
                i = claim.First().Type;
            }

            var Token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Ok(new { Token = Token, typeOfClaim = i, user = user.UserName });
        }
        [HttpPut("EditUser")]
        public IActionResult EditUser(User user)
        {
            var selectedUser = _context.Users.Single(k => k.Id == user.Id);
            selectedUser.FirstName = user.FirstName;
            selectedUser.LastName = user.LastName;
            selectedUser.UserName = user.UserName;
            selectedUser.Password = user.Password;
            selectedUser.Email = user.Email;
            selectedUser.Place = user.Place;
            selectedUser.Street = user.Street;
            _context.Entry(selectedUser).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("EditCraftman")]
        public IActionResult EditCraftman(Craftman Craftman)
        {
            var user = _context.Craftmans.Single(k => k.Id == Craftman.Id);
            user.FirstName = Craftman.FirstName;
            user.LastName = Craftman.LastName;
            user.UserName = Craftman.UserName;
            user.Password = Craftman.Password;
            user.Email = Craftman.Email;
            user.Place = Craftman.Place;
            user.Street = Craftman.Street;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }
    }
}