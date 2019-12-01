using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajstorskiPortal.Models.View
{
    public class postSlucajViewModel
    {
        public Slucaj Slucaj { get; set; }
        public ICollection<Majstor> Majstors { get; set; }
    }
}
