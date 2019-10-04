using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models.View
{
    public class editCenovnikAdvokatVM
    {
        public Cenovnik Cenovnik { get; set; }
        public DateTime datumKreiranja { get; set; }
        public Majstor Majstor { get; set; }
        public int MajstorId { get; set; }
        public Slucaj Slucaj { get; set; }
        public int SlucajId { get; set; }
        public int SlucajStatusId { get; set; }
    }
}
