using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CraftmanPortal.Data;
using CraftmanPortal.Extensons;
using CraftmanPortal.Models;
using CraftmanPortal.Models.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace CraftmanPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CraftmanController : ControllerBase
    {
        private readonly PortalOfCraftsmanDbContext _context;
        readonly UserManager<IdentityUser> _userManager;
        readonly SignInManager<IdentityUser> signInManager;
        public CraftmanController(PortalOfCraftsmanDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            this._userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet("GetAllCraftmans")]
        public IEnumerable<Craftman> GetAllCraftmans()
        {
            var x = _context.Craftmans.Include(i => i.Idenity);
            return x;
        }

        [HttpGet("GetNewNostification")]
        public IActionResult GetNewNostification()
        {
            try
           {
                //   potrebno je setovati za specificnog Craftmana slicno i za ma
                var cliems = User.Claims.First();
                //mozda greska proveriti ovo jer je promenjeno!!!
                var loginUser = _context.Craftmans.Where(x => x.Idenity.Id == cliems.Value).Include(i => i.Idenity).Single();
                                
                var notification = _context.Notifications.Where(n => n.UserId == loginUser.Idenity.Id && n.isRead == false)
                                                               .Include(s => s.Case).ThenInclude(sl => sl.Pictures).ToList();
                                
                foreach (var item in notification)
                {
                    item.isRead = true;
                    _context.Entry(item).State = EntityState.Modified;
                }
                _context.SaveChanges();

                return Ok(notification);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
        
        [HttpGet("GetContractFromCraftmans")]
        public IActionResult GetContractFromCraftmans()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            //ApplicationUser applicationUser = _userManager.GetUserAsync(User);
            //string userEmail = applicationUser?.Email; // will give the user's Email

            var cliems = User.Claims.First();
            var loginUser = _context.Craftmans.Include(i=>i.Idenity).Single(x => x.Idenity.Id == cliems.Value);

            var AllCases = _context.CaseCraftmans.Where(cc => cc.CraftmanIdIndentity == loginUser.Idenity.Id).Include(c => c.Case).ToList();
            var isDeadline = AllCases.Where(ac => ac.CaseStatusId == 1).CheckDeadlineForAnswer();
            if (isDeadline.Any())
            {
                foreach (var item in isDeadline)
                {
                    item.CaseStatusId = 8;
                    _context.Entry(item).State = EntityState.Modified;
                }

                _context.SaveChangesAsync();
            }
            
            var contracts = _context.Contracts.Include(c => c.Case.CaseCraftmans).Include(c => c.Case.User).Include(c => c.Craftman).Where(c => c.CraftmanId == loginUser.Id);
            
            var result = contracts.Select(c => new
            {
                c.Id,
                c.Case.User.FirstName,
                c.Case.User.LastName,
                Case = new
                {
                    c.Case.Id,
                    c.Case.Pictures,
                    c.Case.Description,
                    c.Case.Name
                },
                c.Case.DeadLineForAnswer,
                c.FinishDate,
                c.StartDate,
                c.ChangeCaseDate,
                c.ReciveCase,
                price = c.Price,
                TypeOfPayment = c.TypeOfPayment,
                CaseStatusId = c.Case.CaseCraftmans.First(sm => sm.CaseId == c.Case.Id && sm.CraftmanId == c.Craftman.Id).CaseStatusId,
                CraftmanId = c.CraftmanId

            });


            return Ok(result);
        }
                
        [HttpPost("NewPriceFromCraftman")]
        public async Task<IActionResult> NewPriceFromCraftman([FromBody] Contract noviCenovnikVM)
        {
            var cliems = User.Claims.First();

            var ulogovaniUser = _context.Craftmans.Single(x => x.Idenity.Id == cliems.Value);

            var cenovnik = new Contract
            {
                //IdenityId = cliems.Value,
                CaseId = noviCenovnikVM.CaseId,
                Price = noviCenovnikVM.Price,
                Comment = noviCenovnikVM.Comment,
                TypeOfPayment = noviCenovnikVM.TypeOfPayment,
                StatusId = 1
            };
            _context.Contracts.Add(cenovnik);

            await _context.SaveChangesAsync();

            return Ok(cenovnik);

        }
        
        [HttpPut("AcceptCaseFromCraftman")]
        public async Task<IActionResult> AcceptCaseFromCraftman([FromBody] acceptVM ids)
        {
                var cliems = User.Claims.First();
                var ulogovaniUser = _context.Craftmans.Single(k => k.Idenity.Id == cliems.Value);
                var Case = _context.CaseCraftmans.Where(sl => sl.CraftmanId == ulogovaniUser.Id && sl.CaseId == ids.CaseId)
                                                          .Include(i => i.Case.User.Idenity).Single();

                var notification = Case.CreateNewNotification("accepted", Case.Craftman.FirstName);
                _context.Notifications.Add(notification);

                Case.CaseStatusId = 4;
                _context.Entry(Case).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
           
        }
        
        // KORISTI SE
        [HttpPut("ModificationCaseFromCraftman")]
        public async Task<IActionResult> ModificationCaseFromCraftman([FromBody] Contract CaseCraftman)
        {
            var cliems = User.Claims.First();
            var ulogovaniUser = _context.Craftmans.Single(x => x.Idenity.Id == cliems.Value);

            var caseCraftmans = _context.CaseCraftmans.Where(x => x.CraftmanId == CaseCraftman.CraftmanId && x.CaseId == CaseCraftman.Case.Id)
                                                        .Include(m => m.Craftman).Include(s => s.Case).ThenInclude(k => k.User).ThenInclude(i => i.Idenity).Single();
            
            var newContract = _context.Contracts.Single(c => c.CraftmanId == CaseCraftman.CraftmanId && c.CaseId == CaseCraftman.Case.Id);

            newContract.ModificationContractCraftman(CaseCraftman);

            _context.Entry(newContract).State = EntityState.Modified;

            var notification = caseCraftmans.CreateNewNotification("modification", caseCraftmans.Craftman.FirstName);
            
            _context.Notifications.Add(notification);

            caseCraftmans.CaseStatusId = 7;
            _context.Entry(caseCraftmans).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("RejectCaseFromCraftman")]
        public async Task<IActionResult> RejectCaseFromCraftman([FromBody] acceptVM CaseCraftman)
        {
            var caseCraftmans = _context.CaseCraftmans.Where(x => x.CraftmanId == CaseCraftman.CraftmanId && x.Case.Id == CaseCraftman.CaseId)
                                                      .Include(m => m.Craftman).Include(s => s.Case).ThenInclude(k => k.User).ThenInclude(i => i.Idenity).Single();
            
            var notification = caseCraftmans.CreateNewNotification("rejected", caseCraftmans.Craftman.FirstName);

            _context.Notifications.Add(notification);
            caseCraftmans.CaseStatusId = 5;
            _context.Entry(caseCraftmans).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
        [Authorize(Policy = "AdminCraftman")]
        [HttpPost("PostCategory")]
        public async Task<IActionResult> PostCategory([FromBody] Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("PostSubCategory")]
        public async Task<IActionResult> PostSubCategory([FromBody] Category subCategory)
        {
            _context.Categories.Add(subCategory);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("GetAllCategories")]
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

    }
}