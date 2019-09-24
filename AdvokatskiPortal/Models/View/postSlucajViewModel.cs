using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models.View
{
    public class postSlucajViewModel
    {
        public Slucaj Slucaj { get; set; }
        public ICollection<Majstor> Advokats { get; set; }
    }
}
