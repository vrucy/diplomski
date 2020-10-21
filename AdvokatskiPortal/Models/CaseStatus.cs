using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractorskiPortal.Models
{
    public class CaseStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CaseContractor> CaseContractors { get; set; }
    }
}
