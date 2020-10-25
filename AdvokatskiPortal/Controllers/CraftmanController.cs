using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CraftmanPortal.Data;
using CraftmanPortal.Models;
using CraftmanPortal.Models.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CraftmanPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CraftmanController : ControllerBase
    {
        private readonly PortalOfCraftsmanDbContext _context;
        readonly UserManager<IdentityUser> userManager;
        readonly SignInManager<IdentityUser> signInManager;
        public CraftmanController(PortalOfCraftsmanDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
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
            // potrebno prebaciti isRead na true;
        }

        //[HttpPut("putNewNostifiationReadCraftman")]
        //public async Task<IActionResult> putNewNostifiationReadCraftman([FromBody] CaseCraftman nostification)
        //{
        //    var cliems = User.Claims.FirstOrDefault();
        //    var ulogovaniUser = _context.Users.SingleOrDefault(x => x.Idenity.Id == cliems.Value);
        //    var noviCaseevi = _context.CaseCraftmans.Where(s => s.Craftman.Id == ulogovaniUser.Id && s.isReadOdbijenUser == false);
        //    foreach (var item in noviCaseevi)
        //    {
        //        item.isReadOdbijenUser = true;
        //        _context.Entry(item).State = EntityState.Modified;
        //    }

        //    await _context.SaveChangesAsync();
        //    return Ok(nostification);
        //}
        //[HttpPut("putNewNostifiationRead")]
        //public async Task<IActionResult> putNewNostifiationRead([FromBody] CaseCraftman[] nostification)
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniUser = _context.Craftmans.Single(x => x.Idenity.Id == cliems.Value);
        //    // potrebno prebaciti isRead na true;
        //    var noviCaseevi = _context.CaseCraftmans.Where(s => s.Craftman.Id == ulogovaniUser.Id && s.isRead == false).Include(s => s.Case);
        //    foreach (var item in noviCaseevi)
        //    {
        //        item.isRead = true;
        //        _context.Entry(item).State = EntityState.Modified;
        //    };

        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
        [HttpGet("GetContractFromCraftmans")]
        public IActionResult GetContractFromCraftmans()
        {
            var cliems = User.Claims.First();
            var loginUser = _context.Craftmans.Single(x => x.Idenity.Id == cliems.Value);

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
        //[HttpGet("getCaseNaCekanju")]
        //public IEnumerable<CaseCraftman> getCaseNaCekanju()
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniUser = _context.Craftmans.Single(x => x.Idenity.Id == cliems.Value);

        //    var sviCaseiCraftmana = _context.CaseCraftmans.Where(a => a.Craftman.Id == ulogovaniUser.Id).Include(t => t.Case.Cenovniks).Include(s => s.Case).ThenInclude(c => c.User).Where(q => q.CaseStatusId == 1);

        //    return sviCaseiCraftmana;
        //}

        //[HttpGet("getCaseiPrihvaceni")]
        //public IEnumerable<CaseCraftman> getCaseiPrihvaceni()
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniUser = _context.Craftmans.Single(x => x.Idenity.Id == cliems.Value);

        //    var sviCaseiCraftmana = _context.CaseCraftmans.Where(a => a.Craftman.Id == ulogovaniUser.Id).Include(t => t.Case.Cenovniks).Include(s => s.Case).ThenInclude(c => c.User).Where(q => q.CaseStatusId == 4);

        //    return sviCaseiCraftmana;
        //}

        /// <summary>
        /// odobrenje odbijanje i prihvatanje Casea
        /// </summary>
        /// <param name="CaseCraftman"></param>
        /// <returns></returns>
        /// POTREBNO JE NAPRAVITI U CLIJENTU 2 POZIVA NA KLIKOM EDIT 1 DA SE NAPRAVI NOVI CENOVNIK A DRUGI DA SE EDITUJE STANJE TOG CaseA NA ODGOVOR ADVOKAT
        [HttpPost("NewPriceFromCraftman")]
        public async Task<IActionResult> NewPriceFromCraftman([FromBody] Contract noviCenovnikVM)
        {
            //ne treba ovo proveriti
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
        //KORISNTI SE
        [HttpPut("prepravkaCeneOdCraftmana")]
        public async Task<IActionResult> prepravkaCeneOdCraftmana([FromBody] Contract noviCenovnikVM)
        {
            //editCenovnikCraftmanVM
            //await _context.SaveChangesAsync();
            try
            {
                var cliems = User.Claims.First();
                //noviCenovnikVM.IdenityId = cliems.Value;
                noviCenovnikVM.CaseId = noviCenovnikVM.Case.Id;
                noviCenovnikVM.StatusId = 1;
                //var Case = _context.Cases.Single(x => x.Id == noviCenovnikVM.Case.Id);
                //Case.zavrsetakRada = noviCenovnikVM.Case.zavrsetakRada;
                //_context.Entry(Case).State = EntityState.Modified;

                _context.Entry(noviCenovnikVM).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }

            return Ok();

        }
        [HttpPut("AcceptCaseFromCraftman")]
        public async Task<IActionResult> AcceptCaseFromCraftman([FromBody] acceptVM ids)
        {
                var cliems = User.Claims.First();
                var ulogovaniUser = _context.Craftmans.Single(k => k.Idenity.Id == cliems.Value);
                var Case = _context.CaseCraftmans.Where(sl => sl.CraftmanId == ulogovaniUser.Id && sl.CaseId == ids.CaseId)
                                                          .Include(i => i.Case.User.Idenity).Single();

                var notification = new Notification
                {
                    UserId = Case.Case.User.Idenity.Id,
                    TimeStamp = DateTime.UtcNow.ToLocalTime(),
                    isRead = false,
                    CaseId = Case.CaseId,
                    NotificationText = $"{Case.Craftman.FirstName} je prihvatio da radi na Caseu:  {Case.Case.Name}"
                };
                _context.Notifications.Add(notification);
                Case.CaseStatusId = 4;
                _context.Entry(Case).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
           
        }
        //[HttpPut("prihvatanjeCaseaCraftmana")]
        //public async Task<IActionResult> prihvatanjeCaseaCraftmana([FromBody] CaseCraftman CaseCraftman)
        //{
        //    if (CaseCraftman.CaseStatusId == 1 || CaseCraftman.CaseStatusId == 3 || CaseCraftman.CaseStatusId == 6)
        //    {
        //        var cliems = User.Claims.First();
        //        var ulogovaniUser = _context.Craftmans.Single(k => k.Idenity.Id == cliems.Value);
        //        var Case = _context.CaseCraftmans.Where(sl => sl.CraftmanId == ulogovaniUser.Id && sl.CaseId == CaseCraftman.Case.Id)
        //                                                  .Include(i => i.Case.User.Idenity).Single();
            
        //        var notification = new Notification
        //        {
        //            UserId = Case.Case.User.Idenity.Id,
        //            TFirstNameStamp = DateTime.UtcNow.ToLocalTFirstName(),
        //            isRead = false,
        //            CaseId = Case.CaseId,
        //            NotificationText = $"{Case.Craftman.FirstName} je prihvatio da radi na Caseu:  {Case.Case.Naziv}"
        //        };
        //        _context.Notifications.Add(notification);
        //        Case.CaseStatusId = 4;
        //        _context.Entry(Case).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
        // KORISTI SE
        [HttpPut("ModificationCaseFromCraftman")]
        public async Task<IActionResult> ModificationCaseFromCraftman([FromBody] Contract CaseCraftman)
        {
            //CaseCraftman.Case.ZavrsetakRada = CaseCraftman.Case.ZavrsetakRada;
            var cliems = User.Claims.First();
            var ulogovaniUser = _context.Craftmans.Single(x => x.Idenity.Id == cliems.Value);
            var Case = _context.CaseCraftmans.Where(x => x.CraftmanId == CaseCraftman.CraftmanId && x.CaseId == CaseCraftman.Case.Id)
                                                        .Include(m => m.Craftman).Include(s => s.Case).ThenInclude(k => k.User).ThenInclude(i => i.Idenity).Single();
            
            var newContract = _context.Contracts.Single(c => c.CraftmanId == CaseCraftman.CraftmanId && c.CaseId == CaseCraftman.Case.Id);
                        
            newContract.Price = CaseCraftman.Price;
            newContract.TypeOfPayment = CaseCraftman.TypeOfPayment;
            newContract.Comment = CaseCraftman.Comment;
            newContract.StartDate = CaseCraftman.StartDate;
            newContract.FinishDate = CaseCraftman.FinishDate;
            newContract.isFinal = CaseCraftman.isFinal;
            newContract.ChangeCaseDate = DateTime.UtcNow.ToLocalTime();
            _context.Entry(newContract).State = EntityState.Modified;

            var notification = new Notification
            {
                UserId = Case.Case.User.Idenity.Id,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                CaseId = Case.CaseId,
                NotificationText = $"{Case.Craftman.FirstName} je prepravio Case:  {Case.Case.Name}"
            };
            _context.Notifications.Add(notification);

            Case.CaseStatusId = 7;
            _context.Entry(Case).State = EntityState.Modified;
            //_context.Entry(CaseCraftman.Case).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("RejectCaseFromCraftman")]
        public async Task<IActionResult> RejectCaseFromCraftman([FromBody] acceptVM CaseCraftman)
        {
            var Case = _context.CaseCraftmans.Where(x => x.CraftmanId == CaseCraftman.CraftmanId && x.Case.Id == CaseCraftman.CaseId)
                                                      .Include(m => m.Craftman).Include(s => s.Case).ThenInclude(k => k.User).ThenInclude(i => i.Idenity).Single();
            var notification = new Notification
            {
                UserId = Case.Case.User.Idenity.Id,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                CaseId = Case.CaseId,
                NotificationText = $"{Case.Craftman.FirstName} je odbio Case:  {Case.Case.Name}"
            };
            _context.Notifications.Add(notification);
            Case.CaseStatusId = 5;
            _context.Entry(Case).State = EntityState.Modified;
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