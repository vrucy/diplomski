using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CraftmanPortal.Data;
using CraftmanPortal.Models;
using Microsoft.AspNetCore.Identity;
using CraftmanPortal.Models.View;
using CraftmanPortal.Extensons;
using Microsoft.AspNetCore.Authorization;

namespace CraftmanPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PortalOfCraftsmanDbContext _context;
        readonly UserManager<IdentityUser> userManager;
        //readonly UserManager<ApplicationUser> _userManager;
        public UserController(PortalOfCraftsmanDbContext context, UserManager<IdentityUser> userManager/*, UserManager<ApplicationUser> manager*/)
        {
            _context = context;
            this.userManager = userManager;
        }
        // KORISTI SE
        [Authorize]
        [HttpGet("GetAllCaseForUser")]
        public IEnumerable<Case> GetAllCaseForUser()
        {
            //try
            //{
                var x = User.Claims.FirstOrDefault().Value;
                
                var korsinikCaseevi = _context.Cases.Where(k => k.User.Idenity.Id == x).Include(sm => sm.CaseCraftmans).Include(k => k.Category).Include(s =>s.Pictures);

                var noviCaseevi = korsinikCaseevi.Where(y => y.CaseCraftmans == null);

                return korsinikCaseevi;
            //}
            //catch (Exception e)
            //{

            //    throw;
            //}

        }
        [HttpGet("GetCaseById/{id}")]
        public Case GetCaseById([FromRoute]int id)
        {
            return _context.Cases.Where(s => s.Id == id).Include(sl => sl.Pictures).Single();
        }
        // GET: api/User/5
        [Authorize]
        [HttpGet("GetAllCaseCraftmanForUser")]
        public IActionResult GetAllCaseCraftmanForUser()
        {
            var x = User.Claims.FirstOrDefault().Value;
            var korsinikCaseevi = _context.Cases.Where(k => k.User.Idenity.Id == x).Include(c => c.Contracts).Include(k => k.User).ThenInclude(i => i.Idenity).ToList();
            
            var test = korsinikCaseevi.SelectMany(Case => _context.CaseCraftmans.Where(d => d.CaseId == Case.Id).Include(a => a.Craftman.Idenity).Include(s => s.Case)
                                        .ThenInclude(sl => sl.Pictures));
            

                var result = test.Select(t =>
                {
                    return new
                    {
                        t.Craftman.FirstName,
                        t.Craftman.LastName,
                        CraftmanId = t.CraftmanId,
                        contracts = HandleContract(t.Case.Contracts.Single(s => s.CraftmanId== t.CraftmanId && s.CaseId == t.Case.Id)),
                        t.CaseStatusId,
                        idCraftman = t.Craftman.Idenity.Id,
                        Case = new
                        {
                            Id = t.CaseId,
                            t.Case.Name,
                            t.Case.Description,
                            Pictures = t.Case.Pictures.Select(picture => new { picture.PictureBytes}),
                            t.CreationDate
                        }
                    };
                }).ToList();
           
            return Ok(result);
        }
        private object HandleContract(Contract contract)
        {
            if (contract == null)
                return null;

            return new
            {
                contract.Price,
                contract.FinishDate,
                contract.StartDate,
                contract.TypeOfPayment,
                contract.Comment,
                contract.ReciveCase,
                contract.ChangeCaseDate
            };
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return Ok(User);
        }
        [HttpGet("GetNewNostifiation")]
        public IActionResult GetNewNostifiation()
        {
            try
            {
                var cliems = User.Claims.FirstOrDefault();
                var loginUser = _context.Users.Where(x => x.Idenity.Id == cliems.Value).Include(i => i.Idenity).Single();
                //var noviCaseevi = _context.CaseCraftmans.Where(s => s.Case.User.Id == ulogovaniUser.Id && s.isRead == false).Include(m => m.Craftman).Include(s => s.Case).ToList();

                var notification = _context.Notifications.Where(n => n.UserId == loginUser.Idenity.Id && n.isRead == false)
                                                         .Include(s => s.Case).ThenInclude(sl => sl.Pictures).ToList().ToList();

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
        // KORISTI SE
        [HttpGet("GetAllCraftmans")]
        public IActionResult GetAllCraftmani()
        {

            try
            {
                var x = _context.Craftmans.Include(k => k.Categories).ThenInclude(ka=>ka.Category).Include(i => i.Idenity);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetContractsForUser")]
        public IEnumerable<CaseCraftman> GetContractsForUser()
        {

            var x = User.Claims.FirstOrDefault().Value;
            var korsinikCaseevi = _context.Cases.Where(k => k.User.Idenity.Id == x).Select(i => i.Id);
            var f = _context.CaseCraftmans.Where(d => d.CaseId == korsinikCaseevi.FirstOrDefault()).Include(a => a.Craftman.Idenity).Include(s => s.Case).ThenInclude(c => c.Contracts);

            return f;

        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = User.Id }, User);
        }
        // KORISTI SE
        [HttpPost("CreateCase")]
        public async Task<IActionResult> CreateCase([FromBody] Case Case)
        {
            try
            {
                var cliems = User.Claims.First();
                var q = _context.Users.Single(x => x.Idenity.Id == cliems.Value);
                Case.UserId = q.Id;

                _context.Cases.Include(y => y.User.Id == q.Id);
                _context.Cases.Add(Case);


                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCase", new { id = Case.Id }, Case);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAllCategories")]
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }
        //  KORISTI SE  
        [HttpPost("PostCaseCraftmanima")]
        public IActionResult PostCaseCraftmanima([FromBody] postCaseCraftmanaWithContractViewModel CaseVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in CaseVM.Craftmans)
            {

                if (_context.CaseCraftmans.Where(s => s.CraftmanId == item.Id && s.CaseId == CaseVM.Case.Id).FirstOrDefault() != null)
                {
                    string mess = "Vec ste dodali za svoj Case Craftmanima: " + item.FirstName + " " + item.LastName;

                    return BadRequest(new { message = "You allready send Case Craftmans: " + item.FirstName + " " + item.LastName });
                }
            }

            try
            {
                var cliems = User.Claims.First();
                var loginUser = _context.Users.Single(x => x.Idenity.Id == cliems.Value);
                _context.CaseCraftmans.Include(q => q.Case.User == loginUser);
                _context.CaseCraftmans.Include(m => m.Craftman.Idenity);
                
                CaseVM.User = loginUser;
                CaseVM.UserId = loginUser.Id;
                // CaseVM.Case.Cenovniks = CaseVM.Cenovniks;
                CaseVM.Case.User = CaseVM.User;

                foreach (Craftman Craftman in CaseVM.Craftmans)
                {
                    var newCaseCraftman = new CaseCraftman
                    {
                        CraftmanId = Craftman.Id,
                        CraftmanIdIndentity = Craftman.Idenity.Id,
                        CreationDate = DateTime.UtcNow.ToLocalTime(),
                        CaseId = CaseVM.Case.Id,
                        CaseStatusId = 1
                    };
                    
                    var notification = newCaseCraftman.CreateNewNotification("created", loginUser.FirstName);

                    var cenovnik = new Contract
                    {
                        CraftmanId = Craftman.Id,
                        StatusId = 1,
                        CaseId = CaseVM.Case.Id,
                        ReciveCase = DateTime.UtcNow.ToLocalTime()
                    };
                    _context.CaseCraftmans.Add(newCaseCraftman);
                    _context.Contracts.Add(cenovnik);
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
        [HttpPut("PostNewPriceFromUser")]
        public async Task<IActionResult> PostNewPriceFromUser([FromBody] Contract newContract)
        {

            try
            {
                var contract = _context.Contracts.Single(co => co.CraftmanId == newContract.CraftmanId && co.CaseId == newContract.Case.Id);
                var cliems = User.Claims.First();
                var loginUser = _context.Users.Single(x => x.Idenity.Id == cliems.Value);
                
                var modContract = contract.ModificationContractUser(newContract);
                _context.Entry(modContract).State = EntityState.Modified;

                var caseCraftmans = _context.CaseCraftmans.Where(x => x.CraftmanId == newContract.CraftmanId && x.CaseId == newContract.Case.Id)
                                                          .Include(m =>m.Craftman).ThenInclude(i=>i.Idenity).Include(s => s.Case).ThenInclude(k=>k.User).Single();

                var notification = caseCraftmans.CreateNewNotification("modification", loginUser.FirstName);
                
                _context.Notifications.Add(notification);
                caseCraftmans.CaseStatusId = 6;
                _context.Entry(caseCraftmans).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }

            return Ok();

        }
                
        //KOTISTI SE
        [HttpPut("EditCase")]
        public IActionResult EditCase([FromBody] Case Case)
        {
            var currentCase = _context.Cases.Where(s => s.Id == Case.Id).Include(sli => sli.Pictures).Single();
            
            
            var ids = Case.Pictures.Select(x => x.Id);
            var diff = currentCase.Pictures.Select(q => q.Id).Except(ids);
            List<Picture> pictureRemove = new List<Picture>();
            foreach (var item in diff)
            {
                pictureRemove.AddRange(_context.Pictures.Where(s => s.Id == item));

            }
            _context.Pictures.RemoveRange(pictureRemove);
            _context.SaveChanges();

            foreach (var picture in Case.Pictures)
            {
                if(picture.Id == 0)
                {
                    var newPicture = new Picture()
                     {
                        Name = picture.Name,
                        PictureBytes = picture.PictureBytes,
                        CaseId = Case.Id
                    };
                    _context.Pictures.Add(newPicture);               
                 }
            }
            _context.Entry(currentCase).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(currentCase);
        }
        [HttpPut("AcceptedCaseFromUser")]
        public async Task<IActionResult> AcceptedCaseFromUser([FromBody] acceptVM ids)
        {
            var cliems = User.Claims.First();
            var loginUser = _context.Users.Single(k => k.Idenity.Id == cliems.Value);

            var Case = _context.Cases.SingleOrDefault(c => c.Id == ids.CaseId);
            var CaseCraftman = _context.CaseCraftmans.Where(sl => sl.Case.UserId == loginUser.Id 
                                                  && sl.CaseId == ids.CaseId).Include(m => m.Craftman).ThenInclude(i => i.Idenity);
            foreach (var item in CaseCraftman)
            {
                if (item.CraftmanId == ids.CraftmanId)
                {
                    item.CaseStatusId = 2;
                }
                else
                {
                    item.CaseStatusId = 3;

                    var reject = item.CreateNewNotification("reject", loginUser.FirstName);
                }
                _context.Entry(item).State = EntityState.Modified;
            }
            var Craftman = _context.Craftmans.Where(x => x.Id == ids.CraftmanId).Include(i => i.Idenity).Single();

            var notification = Craftman.AcceptNotification(Case);
            
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("RejectCaseFromUser")]
        public async Task<IActionResult> RejectCaseFromUser([FromBody] acceptVM rejectCase)
        {
            var cliems = User.Claims.First();
            var loginUser = _context.Users.Single(k => k.Idenity.Id == cliems.Value);

            var Case = _context.CaseCraftmans.Where(x => x.CraftmanId == rejectCase.CraftmanId&& x.Case.Id == rejectCase.CaseId).Include(s=>s.Case).Single();
            
            var notification = Case.CreateNewNotification("reject", loginUser.FirstName);
            _context.Notifications.Add(notification);

            Case.CaseStatusId = 3;
            _context.Entry(Case).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}