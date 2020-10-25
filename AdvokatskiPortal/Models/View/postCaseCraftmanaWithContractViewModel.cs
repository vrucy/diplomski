using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CraftmanPortal.Models.View
{
    public class postCaseCraftmanaWithContractViewModel
    {
        public Case Case { get; set; }
        public ICollection<Craftman> Craftmans { get; set; }
        public string Descriptions { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<CaseCraftman> CaseCraftmans { get; set; }

    }
}
