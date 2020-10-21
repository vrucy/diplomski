using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractorskiPortal.Data;
using ContractorskiPortal.Models;
using Microsoft.AspNetCore.Identity;
using ContractorskiPortal.Models.View;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ContractorskiPortal.Controllers
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
        [HttpGet("getAllCaseForUser")]
        public IEnumerable<Case> getAllCaseForUser()
        {
            try
            {
                var x = User.Claims.FirstOrDefault().Value;
                var korsinikCaseevi = _context.Cases.Where(k => k.User.Idenity.Id == x).Include(sm => sm.CaseContractors).Include(k => k.Category).Include(s =>s.Slike);

                var noviCaseevi = korsinikCaseevi.Where(y => y.CaseContractors == null);

                return korsinikCaseevi;
            }
            catch (Exception e)
            {

                throw;
            }

        }
        [HttpGet("getCaseById/{id}")]
        public Case GetCaseById([FromRoute]int id)
        {
            return _context.Cases.Where(s => s.Id == id).Include(sl => sl.Slike).Single();
        }
        // GET: api/User/5
        [HttpGet("getAllCaseContractorForUser")]
        public IActionResult getAllCaseContractorForUser()
        {
            var x = User.Claims.FirstOrDefault().Value;
            var korsinikCaseevi = _context.Cases.Where(k => k.User.Idenity.Id == x).Include(c => c.Contracts).Include(k => k.User).ThenInclude(i => i.Idenity).ToList();
            
            var test = korsinikCaseevi.SelectMany(Case => _context.CaseContractors.Where(d => d.CaseId == Case.Id).Include(a => a.Contractor.Idenity).Include(s => s.Case)
                                        .ThenInclude(sl => sl.Slike));
            

                var result = test.Select(t =>
                {
                    return new
                    {
                        t.Contractor.FirstName,
                        t.Contractor.LastName,
                        ContractorId = t.ContractorId,
                        cenovnici = HandleCenovnik(t.Case.Contracts.Single(s => s.ContractorId== t.ContractorId && s.CaseId == t.Case.Id)),
                        //vrstaPlacanja = t.Case.Cenovniks != null ? t.Case.Cenovniks.FirstOrDefault().vrstaPlacanja : null,
                        //kolicina = t.Case.Cenovniks != null ? t.Case.Cenovniks.FirstOrDefault().kolicina : null,
                        //idContractoraCenovnik = t.Case.Cenovniks != null ? t.Case.Cenovniks.FirstOrDefault().IdenityId : null,
                        //cenovnikId = t.Case.Cenovniks.FirstOrDefault().Id,
                        //t.Case.Cenovniks.FirstOrDefault().StatusId,
                        // TREBA UBACITI ZAVRSETAK RADA
                        //t.ZavrsetakRada,
                        t.CaseStatusId,
                        idContractor = t.Contractor.Idenity.Id,
                        Case = new
                        {
                            Id = t.CaseId,
                            t.Case.Name,
                            t.Case.Description,
                            Slike = t.Case.Slike.Select(slika => new { slika.PictureBytes}),
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
                cenovnik.QuantitySize,
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
        [HttpGet("getNewNostifiation")]
        public IActionResult getNewNostifiationCase()
        {
            try
            {
                var cliems = User.Claims.FirstOrDefault();
                var ulogovaniUser = _context.Users.Where(x => x.Idenity.Id == cliems.Value).Include(i => i.Idenity).Single();
                //var noviCaseevi = _context.CaseContractors.Where(s => s.Case.User.Id == ulogovaniUser.Id && s.isRead == false).Include(m => m.Contractor).Include(s => s.Case).ToList();

                var notification = _context.Notifications.Where(n => n.UserId == ulogovaniUser.Idenity.Id && n.isRead == false);

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
        //public IActionResult putNewNostifiationReadUser([FromBody] List<CaseContractor> nostification)
        //{
        //    var cliems = User.Claims.FirstOrDefault();
        //    var ulogovaniUser = _context.Users.SingleOrDefault(x => x.Idenity.Id == cliems.Value);

        //    var noviCaseevi = _context.CaseContractors.Where(s => s.Case.User.Id == ulogovaniUser.Id && s.isRead == false).Include(m => m.Contractor).Include(s => s.Case).ToList();

        //    List<string> nost = noviCaseevi.Select(s => "dobili ste novi Case " + s.Case.Opis + " od " + s.Contractor.FirstName + " " + s.Contractor.PrezFirstName).ToList();
        //    var odbijeniCaseevi = _context.CaseContractors.Where(os => os.Case.User.Id == ulogovaniUser.Id && os.isReadOdbijenContractor == false);
        //    nost = odbijeniCaseevi.Select(o => o.Contractor.FirstName + " " + o.Contractor.PrezFirstName + " vas je odbio za Case " + o.Case.Opis).ToList();



        //    return Ok(nost);
        //    //await _context.SaveChangesAsync();
            
        //}
       
        //[HttpPut("putNewNostifiationRead")]
        //public async Task<IActionResult> putNewNostifiationRead([FromBody] CaseContractor nostification)
        //{
        //    var cliems = User.Claims.FirstOrDefault();
        //    var ulogovaniUser = _context.Users.SingleOrDefault(x => x.Idenity.Id == cliems.Value);
        //    var noviCaseevi = _context.CaseContractors.Where(s => s.Contractor.Id == ulogovaniUser.Id && s.isRead == false );
        //    foreach (var item in noviCaseevi)
        //    {
        //        item.isRead = true;
        //        _context.Entry(item).State = EntityState.Modified;
        //    }

        //    await _context.SaveChangesAsync();
        //    return Ok(nostification);
        //}
        // KORISTI SE
        [HttpGet("getAllContractori")]
        public IActionResult getAllContractori()
        {

            try
            {
                var x = _context.Contractors.Include(k => k.Categories).ThenInclude(ka=>ka.Category).Include(i => i.Idenity);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetUgovorsForUser")]
        public IEnumerable<CaseContractor> GetUgovorsForUser()
        {

            var x = User.Claims.FirstOrDefault().Value;
            var korsinikCaseevi = _context.Cases.Where(k => k.User.Idenity.Id == x).Select(i => i.Id);
            var f = _context.CaseContractors.Where(d => d.CaseId == korsinikCaseevi.FirstOrDefault()).Include(a => a.Contractor.Idenity).Include(s => s.Case).ThenInclude(c => c.Contracts);

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
        [HttpPost("kreiranjeCasea")]
        public async Task<IActionResult> kreiranjeCasea([FromBody] Case Case)
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

        [HttpGet("getAllKategorije")]
        public IEnumerable<Category> getAllKategorije()
        {
            return _context.Categories;
        }
        //  KORISTI SE  
        [HttpPost("postCaseContractorima")]
        public IActionResult postCaseContractorima([FromBody] postCaseContractoraSaCenovnikomViewModel CaseVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in CaseVM.Contractors)
            {

                if (_context.CaseContractors.Where(s => s.ContractorId == item.Id && s.CaseId == CaseVM.Case.Id).FirstOrDefault() != null)
                {
                    string mess = "Vec ste dodali za svoj Case Contractorima: " + item.FirstName + " " + item.LastName;

                    return StatusCode(404, new { message = mess, customStatusCode = 999 });
                }
            }

            try
            {
                var cliems = User.Claims.First();
                var ulogovaniUser = _context.Users.Single(x => x.Idenity.Id == cliems.Value);
                _context.CaseContractors.Include(q => q.Case.User == ulogovaniUser);
                _context.CaseContractors.Include(m => m.Contractor.Idenity);
                


                CaseVM.User = ulogovaniUser;
                CaseVM.UserId = ulogovaniUser.Id;
                // CaseVM.Case.Cenovniks = CaseVM.Cenovniks;
                CaseVM.Case.User = CaseVM.User;

                foreach (Contractor Contractor in CaseVM.Contractors)
                {
                    var newCaseContractor = new CaseContractor
                    {
                        ContractorId = Contractor.Id,
                        ContractorIdIndentity = Contractor.Idenity.Id,
                        CreationDate = DateTime.UtcNow.ToLocalTime(),
                        CaseId = CaseVM.Case.Id,
                        CaseStatusId = 1
                    };
                    var notification = new Notification
                    {
                        UserId = newCaseContractor.ContractorIdIndentity,
                        TimeStamp = DateTime.UtcNow.ToLocalTime(),
                        isRead = false,
                        CaseId = CaseVM.Case.Id,
                        NotificationText = $"{ulogovaniUser.FirstName} vam je dodao Case:  {CaseVM.Case.Name}"
                    };
                    var cenovnik = new Contract
                    {
                        ContractorId = Contractor.Id,
                        StatusId = 1,
                        CaseId = CaseVM.Case.Id,
                        ReciveCase = DateTime.UtcNow.ToLocalTime()
                        //zavrsetakRada = null
                    };
                    _context.CaseContractors.Add(newCaseContractor);
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
        [HttpPut("postavljanjeNoveCeneOdUsera")]
        public async Task<IActionResult> postavljanjeNoveCeneOdUsera([FromBody] Contract noviCenovnikVM)
        {

            try
            {
                var noviCenovnik = _context.Contracts.Single(c => c.ContractorId == noviCenovnikVM.ContractorId && c.CaseId == noviCenovnikVM.Case.Id);
                var cliems = User.Claims.First();
                var ulogovaniUser = _context.Users.Single(x => x.Idenity.Id == cliems.Value);
                noviCenovnik.QuantitySize = noviCenovnikVM.QuantitySize;
                noviCenovnik.TypeOfPayment = noviCenovnikVM.TypeOfPayment;
                noviCenovnik.Comment = noviCenovnikVM.Comment;
                noviCenovnik.ChangeCaseDate = DateTime.UtcNow.ToLocalTime();
                _context.Entry(noviCenovnik).State = EntityState.Modified;
                var Case = _context.CaseContractors.Where(x => x.ContractorId == noviCenovnikVM.ContractorId && x.CaseId == noviCenovnikVM.Case.Id)
                                                          .Include(m =>m.Contractor).ThenInclude(i=>i.Idenity).Include(s => s.Case).ThenInclude(k=>k.User).Single();
                var notification = new Notification
                {
                    UserId = Case.Contractor.Idenity.Id,
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
        public async Task<IActionResult> prepravkaCaseaUser([FromBody] Contract CaseContractor)
        {

            try
            {
                // rucno nabajdovati Case Id slicno i gore treba
                // CaseContractor.CaseId = CaseContractor.Case.Id;
                
                var Case = _context.CaseContractors.Single(x => x.ContractorId == CaseContractor.ContractorId && x.CaseId == CaseContractor.Case.Id);
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
        [HttpPut("editCase")]
        public IActionResult editCase([FromBody] Case Case)
        {
            var currentCase = _context.Cases.Where(s => s.Id == Case.Id).Include(sli => sli.Slike).Single();
            
            
            var ids = Case.Slike.Select(x => x.Id);
            var diff = currentCase.Slike.Select(q => q.Id).Except(ids);
            List<Picture> slikeRemove = new List<Picture>();
            foreach (var item in diff)
            {
                slikeRemove.AddRange(_context.Slikas.Where(s => s.Id == item));

            }
            _context.Slikas.RemoveRange(slikeRemove);
            _context.SaveChanges();

            foreach (var slika in Case.Slike)
            {
                if(slika.Id == 0)
                {
                    var novaSlika = new Picture()
                     {
                        Name = slika.Name,
                        PictureBytes = slika.PictureBytes,
                        CaseId = Case.Id
                    };
                    _context.Slikas.Add(novaSlika);               
                 }
            }
            _context.Entry(currentCase).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(currentCase);
        }
        [HttpPut("prihvacenCaseUser")]
        public async Task<IActionResult> prihvacenCaseUser([FromBody] acceptVM ids)
        {
            var cliems = User.Claims.First();
            var ulogovaniUser = _context.Users.Single(k => k.Idenity.Id == cliems.Value);

            var Case = _context.Cases.SingleOrDefault(c => c.Id == ids.CaseId);
            var CaseContractor = _context.CaseContractors.Where(sl => sl.Case.UserId == ulogovaniUser.Id 
                                                  && sl.CaseId == ids.CaseId).Include(m => m.Contractor).ThenInclude(i => i.Idenity);
            foreach (var item in CaseContractor)
            {
                if (item.ContractorId == ids.ContractorId)
                {
                    item.CaseStatusId = 2;
                }
                else
                {
                    item.CaseStatusId = 3;
                    var odbijen = new Notification
                    {
                        UserId = item.Contractor.Idenity.Id,
                        TimeStamp = DateTime.UtcNow.ToLocalTime(),
                        isRead = false,
                        CaseId = Case.Id,
                        NotificationText = $"{ulogovaniUser.FirstName} je odbio Case:  {Case.Name}"
                    };
                }
                _context.Entry(item).State = EntityState.Modified;
            }
            var Contractor = _context.Contractors.Where(x => x.Id == ids.ContractorId).Include(i => i.Idenity).Single();
            var notification = new Notification
            {
                UserId = Contractor.Idenity.Id,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                CaseId = Case.Id,
                NotificationText = $"{ulogovaniUser.FirstName} je prihvatio Case:  {Case.Name}"
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("odbijenCaseOdUsera")]
        public async Task<IActionResult> odbijenCaseOdUsera([FromBody] acceptVM odbijenCase)
        {
            var cliems = User.Claims.First();
            var ulogovaniUser = _context.Users.Single(k => k.Idenity.Id == cliems.Value);

            var Case = _context.CaseContractors.Where(x => x.ContractorId == odbijenCase.ContractorId&& x.Case.Id == odbijenCase.CaseId).Include(s=>s.Case).Single();
            var notification = new Notification
            {
                UserId = Case.ContractorIdIndentity,
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