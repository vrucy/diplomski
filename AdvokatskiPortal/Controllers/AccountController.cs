using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ContractorskiPortal.Data;
using ContractorskiPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using ContractorskiPortal.Models.View;
using Microsoft.EntityFrameworkCore;

namespace ContractorskiPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize("AdminContractor")]

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
        //[Authorize(Policy = "AdminContractor")]
        [HttpPost("registrationContractor")]
        public async Task<IActionResult> RegistarContractor([FromBody] postContractor Contractor)
        {

            var newContractor = new Contractor
            {
                Email = Contractor.Email,
                FirstName = Contractor.FirstName,
                Place = Contractor.Place,
                Password = Contractor.Password,
                LastName = Contractor.LastName,
                Street = Contractor.Street,
                UserName = Contractor.UserName
            };
            _context.Contractors.Add(newContractor);
            
            foreach (var item in Contractor.Categories)
            {
                var newMajtorKategorije = new ContractCategory
                {
                    CategoryId = item.Id,
                    ContractorId = newContractor.Id,
                };
                _context.ContractCategores.Add(newMajtorKategorije);
            }
            var appUser = new ApplicationUser { UserName = Contractor.UserName, Email = Contractor.Email };

            var result = await userManager.CreateAsync(appUser, Contractor.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await signInManager.SignInAsync(appUser, isPersistent: false);
            newContractor.Idenity = appUser;

            var user = await userManager.FindByNameAsync(Contractor.UserName);

            await userManager.AddClaimAsync(appUser, new Claim("RegularContractor", appUser.Id));
            await userManager.AddClaimAsync(appUser, new Claim("AdminContractor", appUser.Id));
            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registar([FromBody] User User)
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
        [HttpGet("getUser")]
        public IActionResult getUser()
        {
            var cliems = User.Claims.First();
            var loginUser = _context.Users.Single(x => x.Idenity.Id == cliems.Value);

            return Ok(loginUser);
        }
        [HttpGet("getContractor")]
        public IActionResult getContractor()
        {
            var cliems = User.Claims.First();
            var Contractor = _context.Contractors.Single(x => x.Idenity.Id == cliems.Value);

            return Ok(Contractor);
        }
        [HttpGet("getCurrentUser/{userName}")]
        public async Task<IActionResult> getCurrentUser([FromRoute] string userName)
        {
            var User = _context.Users.Where(x => x.UserName == userName);
            var Contractor = _context.Contractors.Where(x => x.UserName == userName);
            if (User != null)
            {
                return Ok(User);
            }
            else
            {
                return Ok(Contractor);
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
            if (claim.Where(c => c.Type == "AdminContractor").SingleOrDefault() != null)
            {
                i = claim.Where(c => c.Type == "AdminContractor").Single().Type;
            }
            else
            {
                i = claim.First().Type;
            }

            var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Ok(new { Token = token, typeOfClaim = i, user = user.UserName });
        }
        [HttpPut("editUser")]
        public IActionResult editUser(User user)
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
        [HttpPut("editContractor")]
        public IActionResult editContractor(Contractor Contractor)
        {
            var user = _context.Contractors.Single(k => k.Id == Contractor.Id);
            user.FirstName = Contractor.FirstName;
            user.LastName = Contractor.LastName;
            user.UserName = Contractor.UserName;
            user.Password = Contractor.Password;
            user.Email = Contractor.Email;
            user.Place = Contractor.Place;
            user.Street = Contractor.Street;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }
    }
}