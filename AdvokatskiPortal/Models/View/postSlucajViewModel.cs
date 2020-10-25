using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CraftmanPortal.Models.View
{
    public class postCaseViewModel
    {
        public Case Case { get; set; }
        public ICollection<Craftman> Craftmans { get; set; }
    }
}
