using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContractorskiPortal.Data;
using ContractorskiPortal.Models;
using ContractorskiPortal.Models.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractorskiPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ContractorController : ControllerBase
    {
        private readonly PortalOfCraftsmanDbContext _context;
        readonly UserManager<IdentityUser> userManager;
        readonly SignInManager<IdentityUser> signInManager;
        public ContractorController(PortalOfCraftsmanDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet("getAllContractori")]
        public IEnumerable<Contractor> getAllContractori()
        {
            var x = _context.Contractors.Include(i => i.Idenity);
            return x;
        }

        [HttpGet("getNewNostifiation")]
        public IActionResult getNewNostifiationCase()
        {
            try
            {
                //   potrebno je setovati za specificnog Contractora slicno i za ma
                var cliems = User.Claims.FirstOrDefault();
                //mozda greska proveriti ovo jer je promenjeno!!!
                var ulogovaniUser = _context.Users.Where(x => x.Idenity.Id == cliems.Value).Single();

                var notification = _context.Notifications.Where(n => n.UserId == ulogovaniUser.Idenity.Id && n.isRead == false)
                                                               .Include(s => s.Case).ThenInclude(sl => sl.Slike).ToList();
                //var result = notification.Select(n => new
                //{
                //    n.Case.Naziv,
                //    n.Case.Opis,
                //    n.Case.Slike,
                //    n.isRead
                //});
                
                foreach (var item in notification)
                {
                    item.isRead = true;
                    _context.Entry(item).State = EntityState.Modified;
                }
                _context.SaveChanges();

                return Ok(notification);
            }
            catch
            {
                return BadRequest();
            }
            // potrebno prebaciti isRead na true;
        }

        //[HttpPut("putNewNostifiationReadContractor")]
        //public async Task<IActionResult> putNewNostifiationReadContractor([FromBody] CaseContractor nostification)
        //{
        //    var cliems = User.Claims.FirstOrDefault();
        //    var ulogovaniUser = _context.Users.SingleOrDefault(x => x.Idenity.Id == cliems.Value);
        //    var noviCaseevi = _context.CaseContractors.Where(s => s.Contractor.Id == ulogovaniUser.Id && s.isReadOdbijenUser == false);
        //    foreach (var item in noviCaseevi)
        //    {
        //        item.isReadOdbijenUser = true;
        //        _context.Entry(item).State = EntityState.Modified;
        //    }

        //    await _context.SaveChangesAsync();
        //    return Ok(nostification);
        //}
        //[HttpPut("putNewNostifiationRead")]
        //public async Task<IActionResult> putNewNostifiationRead([FromBody] CaseContractor[] nostification)
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniUser = _context.Contractors.Single(x => x.Idenity.Id == cliems.Value);
        //    // potrebno prebaciti isRead na true;
        //    var noviCaseevi = _context.CaseContractors.Where(s => s.Contractor.Id == ulogovaniUser.Id && s.isRead == false).Include(s => s.Case);
        //    foreach (var item in noviCaseevi)
        //    {
        //        item.isRead = true;
        //        _context.Entry(item).State = EntityState.Modified;
        //    };

        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
        [HttpGet("getUgovorsForContractor")]
        public IActionResult getUgovorsForContractorAsync()
        {
            var cliems = User.Claims.First();
            var ulogovaniUser = _context.Contractors.Single(x => x.Idenity.Id == cliems.Value);

            var cenovnici = _context.Contracts.Include(c => c.Case.CaseContractors).Include(c => c.Case.User).Include(c => c.Contractor).Where(c => c.ContractorId == ulogovaniUser.Id);

            var result = cenovnici.Select(c => new
            {
                c.Id,
                c.Case.User.FirstName,
                c.Case.User.LastName,
                Case = new
                {
                    c.Case.Id,
                    c.Case.Slike,
                    c.Case.Description,
                    c.Case.Name
                },
                c.Case.DeadLineForAnswer,
                c.FinishDate,
                c.StartDate,
                c.ChangeCaseDate,
                c.ReciveCase,
                Kolicina = c.QuantitySize,
                VrstaPlacanja = c.TypeOfPayment,
                CaseStatusId = c.Case.CaseContractors.First(sm => sm.CaseId == c.Case.Id && sm.ContractorId == c.Contractor.Id).CaseStatusId,
                ContractorId = c.ContractorId

            });


            return Ok(result);
        }
        //[HttpGet("getCaseNaCekanju")]
        //public IEnumerable<CaseContractor> getCaseNaCekanju()
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniUser = _context.Contractors.Single(x => x.Idenity.Id == cliems.Value);

        //    var sviCaseiContractora = _context.CaseContractors.Where(a => a.Contractor.Id == ulogovaniUser.Id).Include(t => t.Case.Cenovniks).Include(s => s.Case).ThenInclude(c => c.User).Where(q => q.CaseStatusId == 1);

        //    return sviCaseiContractora;
        //}

        //[HttpGet("getCaseiPrihvaceni")]
        //public IEnumerable<CaseContractor> getCaseiPrihvaceni()
        //{
        //    var cliems = User.Claims.First();
        //    var ulogovaniUser = _context.Contractors.Single(x => x.Idenity.Id == cliems.Value);

        //    var sviCaseiContractora = _context.CaseContractors.Where(a => a.Contractor.Id == ulogovaniUser.Id).Include(t => t.Case.Cenovniks).Include(s => s.Case).ThenInclude(c => c.User).Where(q => q.CaseStatusId == 4);

        //    return sviCaseiContractora;
        //}

        /// <summary>
        /// odobrenje odbijanje i prihvatanje Casea
        /// </summary>
        /// <param name="CaseContractor"></param>
        /// <returns></returns>
        /// POTREBNO JE NAPRAVITI U CLIJENTU 2 POZIVA NA KLIKOM EDIT 1 DA SE NAPRAVI NOVI CENOVNIK A DRUGI DA SE EDITUJE STANJE TOG CaseA NA ODGOVOR ADVOKAT
        [HttpPost("postavljanjeNoveCeneOdContractora")]
        public async Task<IActionResult> postavljanjeNoveCeneOdContractora([FromBody] Contract noviCenovnikVM)
        {
            //ne treba ovo proveriti
            var cliems = User.Claims.First();
            var ulogovaniUser = _context.Contractors.Single(x => x.Idenity.Id == cliems.Value);

            var cenovnik = new Contract
            {
                //IdenityId = cliems.Value,
                CaseId = noviCenovnikVM.CaseId,
                QuantitySize = noviCenovnikVM.QuantitySize,
                Comment = noviCenovnikVM.Comment,
                TypeOfPayment = noviCenovnikVM.TypeOfPayment,
                StatusId = 1
            };
            _context.Contracts.Add(cenovnik);

            await _context.SaveChangesAsync();

            return Ok(cenovnik);

        }
        //KORISNTI SE
        [HttpPut("prepravkaCeneOdContractora")]
        public async Task<IActionResult> prepravkaCeneOdContractora([FromBody] Contract noviCenovnikVM)
        {
            //editCenovnikContractorVM
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
        [HttpPut("prihvatanjeCaseaContractora")]
        public async Task<IActionResult> prihvatanjeCaseaContractor([FromBody] acceptVM ids)
        {
                var cliems = User.Claims.First();
                var ulogovaniUser = _context.Contractors.Single(k => k.Idenity.Id == cliems.Value);
                var Case = _context.CaseContractors.Where(sl => sl.ContractorId == ulogovaniUser.Id && sl.CaseId == ids.CaseId)
                                                          .Include(i => i.Case.User.Idenity).Single();

                var notification = new Notification
                {
                    UserId = Case.Case.User.Idenity.Id,
                    TimeStamp = DateTime.UtcNow.ToLocalTime(),
                    isRead = false,
                    CaseId = Case.CaseId,
                    NotificationText = $"{Case.Contractor.FirstName} je prihvatio da radi na Caseu:  {Case.Case.Name}"
                };
                _context.Notifications.Add(notification);
                Case.CaseStatusId = 4;
                _context.Entry(Case).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
           
        }
        //[HttpPut("prihvatanjeCaseaContractora")]
        //public async Task<IActionResult> prihvatanjeCaseaContractora([FromBody] CaseContractor CaseContractor)
        //{
        //    if (CaseContractor.CaseStatusId == 1 || CaseContractor.CaseStatusId == 3 || CaseContractor.CaseStatusId == 6)
        //    {
        //        var cliems = User.Claims.First();
        //        var ulogovaniUser = _context.Contractors.Single(k => k.Idenity.Id == cliems.Value);
        //        var Case = _context.CaseContractors.Where(sl => sl.ContractorId == ulogovaniUser.Id && sl.CaseId == CaseContractor.Case.Id)
        //                                                  .Include(i => i.Case.User.Idenity).Single();
            
        //        var notification = new Notification
        //        {
        //            UserId = Case.Case.User.Idenity.Id,
        //            TFirstNameStamp = DateTime.UtcNow.ToLocalTFirstName(),
        //            isRead = false,
        //            CaseId = Case.CaseId,
        //            NotificationText = $"{Case.Contractor.FirstName} je prihvatio da radi na Caseu:  {Case.Case.Naziv}"
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
        [HttpPut("prepravkaCaseaContractora")]
        public async Task<IActionResult> prepravkaCaseaContractora([FromBody] Contract CaseContractor)
        {
            //CaseContractor.Case.ZavrsetakRada = CaseContractor.Case.ZavrsetakRada;
            var cliems = User.Claims.First();
            var ulogovaniUser = _context.Contractors.Single(x => x.Idenity.Id == cliems.Value);
            var Case = _context.CaseContractors.Where(x => x.ContractorId == CaseContractor.ContractorId && x.CaseId == CaseContractor.Case.Id)
                                                        .Include(m => m.Contractor).Include(s => s.Case).ThenInclude(k => k.User).ThenInclude(i => i.Idenity).Single();
            var newContract = _context.Contracts.Single(c => c.ContractorId == CaseContractor.ContractorId && c.CaseId == CaseContractor.Case.Id);
            newContract.QuantitySize = CaseContractor.QuantitySize;
            newContract.TypeOfPayment = CaseContractor.TypeOfPayment;
            newContract.Comment = CaseContractor.Comment;
            newContract.StartDate = CaseContractor.StartDate;
            newContract.FinishDate = CaseContractor.FinishDate;
            //newContract.isKonacan = CaseContractor.isKonacan;
            newContract.ChangeCaseDate = DateTime.UtcNow.ToLocalTime();
            _context.Entry(newContract).State = EntityState.Modified;

            var notification = new Notification
            {
                UserId = Case.Case.User.Idenity.Id,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                CaseId = Case.CaseId,
                NotificationText = $"{Case.Contractor.FirstName} je prepravio Case:  {Case.Case.Name}"
            };
            _context.Notifications.Add(notification);

            Case.CaseStatusId = 7;
            _context.Entry(Case).State = EntityState.Modified;
            //_context.Entry(CaseContractor.Case).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("odbijanjeCaseaContractora")]
        public async Task<IActionResult> odbijanjeCaseaContractora([FromBody] acceptVM CaseContractor)
        {
            var Case = _context.CaseContractors.Where(x => x.ContractorId == CaseContractor.ContractorId && x.Case.Id == CaseContractor.CaseId)
                                                      .Include(m => m.Contractor).Include(s => s.Case).ThenInclude(k => k.User).ThenInclude(i => i.Idenity).Single();
            var notification = new Notification
            {
                UserId = Case.Case.User.Idenity.Id,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                CaseId = Case.CaseId,
                NotificationText = $"{Case.Contractor.FirstName} je odbio Case:  {Case.Case.Name}"
            };
            _context.Notifications.Add(notification);
            Case.CaseStatusId = 5;
            _context.Entry(Case).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
        [Authorize(Policy = "AdminContractor")]
        [HttpPost("postKategorija")]
        public async Task<IActionResult> postKategorija([FromBody] Category category)
        {
            _context.Categories.Add(category);

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("postPodKategorija")]
        public async Task<IActionResult> PodKategorija([FromBody] Category subCategory)
        {
            _context.Categories.Add(subCategory);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("getAllKategorija")]
        public IEnumerable<Category> GetKategorijas()
        {
            return _context.Categories;
        }

    }
}