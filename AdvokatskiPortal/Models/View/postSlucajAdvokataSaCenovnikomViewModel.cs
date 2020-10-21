using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractorskiPortal.Models.View
{
    public class postCaseContractoraSaCenovnikomViewModel
    {
        public Case Case { get; set; }
        public ICollection<Contractor> Contractors { get; set; }
        public string Opis { get; set; }
        //public Ugovor Ugovor { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<CaseContractor> CaseContractors { get; set; }
        //public ICollection<Cenovnik> Cenovniks { get; set; }

    }
}
