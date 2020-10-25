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
        [HttpGet("GetAllCaseForUser")]
        public IEnumerable<Case> GetAllCaseForUser()
        {
            try
            {
                var x = User.Claims.FirstOrDefault().Value;
                var korsinikCaseevi = _context.Cases.Where(k => k.User.Idenity.Id == x).Include(sm => sm.CaseCraftmans).Include(k => k.Category).Include(s =>s.Pictures);

                var noviCaseevi = korsinikCaseevi.Where(y => y.CaseCraftmans == null);

                return korsinikCaseevi;
            }
            catch (Exception e)
            {

                throw;
            }

        }
        [HttpGet("GetCaseById/{id}")]
        public Case GetCaseById([FromRoute]int id)
        {
            return _context.Cases.Where(s => s.Id == id).Include(sl => sl.Pictures).Single();
        }
        // GET: api/User/5
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
                        contracts = HandleCenovnik(t.Case.Contracts.Single(s => s.CraftmanId== t.CraftmanId && s.CaseId == t.Case.Id)),
                        //vrstaPlacanja = t.Case.Cenovniks != null ? t.Case.Cenovniks.FirstOrDefault().vrstaPlacanja : null,
                        //kolicina = t.Case.Cenovniks != null ? t.Case.Cenovniks.FirstOrDefault().kolicina : null,
                        //idCraftmanaCenovnik = t.Case.Cenovniks != null ? t.Case.Cenovniks.FirstOrDefault().IdenityId : null,
                        //cenovnikId = t.Case.Cenovniks.FirstOrDefault().Id,
                        //t.Case.Cenovniks.FirstOrDefault().StatusId,
                        // TREBA UBACITI ZAVRSETAK RADA
                        //t.ZavrsetakRada,
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
        private object HandleCenovnik(Contract cenovnik)
        {
            if (cenovnik == null)
                return null;

            return new
            {
                cenovnik.Price,
                cenovnik.FinishDate,
                cenovnik.StartDate,
                cenovnik.TypeOfPayment,
                cenovnik.ReciveCase,
                cenovnik.ChangeCaseDate
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
                var ulogovaniUser = _context.Users.Where(x => x.Idenity.Id == cliems.Value).Include(i => i.Idenity).Single();
                //var noviCaseevi = _context.CaseCraftmans.Where(s => s.Case.User.Id == ulogovaniUser.Id && s.isRead == false).Include(m => m.Craftman).Include(s => s.Case).ToList();

                var notification = _context.Notifications.Where(n => n.UserId == ulogovaniUser.Idenity.Id && n.isRead == false)
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
        //[HttpPut("putNewNostifiationReadUser")]
        //public IActionResult putNewNostifiationReadUser([FromBody] List<CaseCraftman> nostification)
        //{
        //    var cliems = User.Claims.FirstOrDefault();
        //    var ulogovaniUser = _context.Users.SingleOrDefault(x => x.Idenity.Id == cliems.Value);

        //    var noviCaseevi = _context.CaseCraftmans.Where(s => s.Case.User.Id == ulogovaniUser.Id && s.isRead == false).Include(m => m.Craftman).Include(s => s.Case).ToList();

        //    List<string> nost = noviCaseevi.Select(s => "dobili ste novi Case " + s.Case.Opis + " od " + s.Craftman.FirstName + " " + s.Craftman.PrezFirstName).ToList();
        //    var odbijeniCaseevi = _context.CaseCraftmans.Where(os => os.Case.User.Id == ulogovaniUser.Id && os.isReadOdbijenCraftman == false);
        //    nost = odbijeniCaseevi.Select(o => o.Craftman.FirstName + " " + o.Craftman.PrezFirstName + " vas je odbio za Case " + o.Case.Opis).ToList();



        //    return Ok(nost);
        //    //await _context.SaveChangesAsync();
            
        //}
       
        //[HttpPut("putNewNostifiationRead")]
        //public async Task<IActionResult> putNewNostifiationRead([FromBody] CaseCraftman nostification)
        //{
        //    var cliems = User.Claims.FirstOrDefault();
        //    var ulogovaniUser = _context.Users.SingleOrDefault(x => x.Idenity.Id == cliems.Value);
        //    var noviCaseevi = _context.CaseCraftmans.Where(s => s.Craftman.Id == ulogovaniUser.Id && s.isRead == false );
        //    foreach (var item in noviCaseevi)
        //    {
        //        item.isRead = true;
        //        _context.Entry(item).State = EntityState.Modified;
        //    }

        //    await _context.SaveChangesAsync();
        //    return Ok(nostification);
        //}
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

                    return StatusCode(404, new { message = mess, customStatusCode = 999 });
                }
            }

            try
            {
                var cliems = User.Claims.First();
                var ulogovaniUser = _context.Users.Single(x => x.Idenity.Id == cliems.Value);
                _context.CaseCraftmans.Include(q => q.Case.User == ulogovaniUser);
                _context.CaseCraftmans.Include(m => m.Craftman.Idenity);
                


                CaseVM.User = ulogovaniUser;
                CaseVM.UserId = ulogovaniUser.Id;
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
                    var notification = new Notification
                    {
                        UserId = newCaseCraftman.CraftmanIdIndentity,
                        TimeStamp = DateTime.UtcNow.ToLocalTime(),
                        isRead = false,
                        CaseId = CaseVM.Case.Id,
                        NotificationText = $"{ulogovaniUser.FirstName} vam je dodao Case:  {CaseVM.Case.Name}"
                    };
                    var cenovnik = new Contract
                    {
                        CraftmanId = Craftman.Id,
                        StatusId = 1,
                        CaseId = CaseVM.Case.Id,
                        ReciveCase = DateTime.UtcNow.ToLocalTime()
                        //zavrsetakRada = null
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
        public async Task<IActionResult> PostNewPriceFromUser([FromBody] Contract noviCenovnikVM)
        {

            try
            {
                var noviCenovnik = _context.Contracts.Single(c => c.CraftmanId == noviCenovnikVM.CraftmanId && c.CaseId == noviCenovnikVM.Case.Id);
                var cliems = User.Claims.First();
                var ulogovaniUser = _context.Users.Single(x => x.Idenity.Id == cliems.Value);
                noviCenovnik.Price = noviCenovnikVM.Price;
                noviCenovnik.TypeOfPayment = noviCenovnikVM.TypeOfPayment;
                noviCenovnik.Comment = noviCenovnikVM.Comment;
                noviCenovnik.ChangeCaseDate = DateTime.UtcNow.ToLocalTime();
                _context.Entry(noviCenovnik).State = EntityState.Modified;
                var Case = _context.CaseCraftmans.Where(x => x.CraftmanId == noviCenovnikVM.CraftmanId && x.CaseId == noviCenovnikVM.Case.Id)
                                                          .Include(m =>m.Craftman).ThenInclude(i=>i.Idenity).Include(s => s.Case).ThenInclude(k=>k.User).Single();
                var notification = new Notification
                {
                    UserId = Case.Craftman.Idenity.Id,
                    TimeStamp = DateTime.UtcNow.ToLocalTime(),
                    isRead = false,
                    CaseId = Case.CaseId,
                    NotificationText = $"{Case.Case.User.FirstName} je prepravio Case:  {Case.Case.Name}"
                };
                Case.CaseStatusId = 6;
                _context.Entry(Case).State = EntityState.Modified;
                _context.Notifications.Add(notification);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }

            return Ok();

        }
        [HttpPut("prepravkaCaseaUser")]
        public async Task<IActionResult> prepravkaCaseaUser([FromBody] Contract CaseCraftman)
        {

            try
            {
                // rucno nabajdovati Case Id slicno i gore treba
                // CaseCraftman.CaseId = CaseCraftman.Case.Id;
                
                var Case = _context.CaseCraftmans.Single(x => x.CraftmanId == CaseCraftman.CraftmanId && x.CaseId == CaseCraftman.Case.Id);
                Case.CaseStatusId = 6;
                _context.Entry(Case).State = EntityState.Modified;
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
            var ulogovaniUser = _context.Users.Single(k => k.Idenity.Id == cliems.Value);

            var Case = _context.Cases.SingleOrDefault(c => c.Id == ids.CaseId);
            var CaseCraftman = _context.CaseCraftmans.Where(sl => sl.Case.UserId == ulogovaniUser.Id 
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
                    var odbijen = new Notification
                    {
                        UserId = item.Craftman.Idenity.Id,
                        TimeStamp = DateTime.UtcNow.ToLocalTime(),
                        isRead = false,
                        CaseId = Case.Id,
                        NotificationText = $"{ulogovaniUser.FirstName} je odbio Case:  {Case.Name}"
                    };
                }
                _context.Entry(item).State = EntityState.Modified;
            }
            var Craftman = _context.Craftmans.Where(x => x.Id == ids.CraftmanId).Include(i => i.Idenity).Single();
            var notification = new Notification
            {
                UserId = Craftman.Idenity.Id,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                CaseId = Case.Id,
                NotificationText = $"{ulogovaniUser.FirstName} je prihvatio Case:  {Case.Name}"
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("RejectCaseFromUser")]
        public async Task<IActionResult> RejectCaseFromUser([FromBody] acceptVM odbijenCase)
        {
            var cliems = User.Claims.First();
            var ulogovaniUser = _context.Users.Single(k => k.Idenity.Id == cliems.Value);

            var Case = _context.CaseCraftmans.Where(x => x.CraftmanId == odbijenCase.CraftmanId&& x.Case.Id == odbijenCase.CaseId).Include(s=>s.Case).Single();
            var notification = new Notification
            {
                UserId = Case.CraftmanIdIndentity,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                CaseId = odbijenCase.CaseId,
                NotificationText = $"{ulogovaniUser.FirstName} je odbio Case:  {Case.Case.Name}"
            };
            _context.Notifications.Add(notification);
            Case.CaseStatusId = 3;
            _context.Entry(Case).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}