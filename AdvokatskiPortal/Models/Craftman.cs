using System.Collections.Generic;

namespace CraftmanPortal.Models
{
    public class Craftman
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
        public ICollection<CaseCraftman> CaseCraftmans { get; set; } = new List<CaseCraftman>();
        public ICollection<Contract> Contracts { get; set; }
    }
}
