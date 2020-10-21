using System.Collections.Generic;

namespace ContractorskiPortal.Models
{
    public class Contractor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Place { get; set; }
        public string Street { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<ContractCategory> Categories { get; set; }
        public ApplicationUser Idenity { get; set; }
        public ICollection<CaseContractor> CaseContractors { get; set; } = new List<CaseContractor>();
        public ICollection<Contract> Contracts { get; set; }
    }
}
