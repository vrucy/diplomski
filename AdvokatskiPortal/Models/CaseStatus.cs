using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CraftmanPortal.Models
{
    public class CaseStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CaseCraftman> CaseCraftmans { get; set; }
    }
}
