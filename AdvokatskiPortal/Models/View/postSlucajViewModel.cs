using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractorskiPortal.Models.View
{
    public class postCaseViewModel
    {
        public Case Case { get; set; }
        public ICollection<Contractor> Contractors { get; set; }
    }
}
