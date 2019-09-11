using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models.View
{
    public class postNewCenovnikFromAdvokatVM
    {
        public DateTime datumKreiranja { get; set; }
        public bool prihvacno { get; set; }
        public string Odgovor { get; set; }
        public bool isRead { get; set; }
        public bool isActive { get; set; }
        public int AdvokatId { get; set; }
        public Advokat Advokat { get; set; }
        public int SlucajId { get; set; }
        public Slucaj Slucaj { get; set; }
        public int SlucajStatusId { get; set; }
        public SlucajStatus SlucajStatus { get; set; }
        public Cenovnik Cenovnik { get; set; }
    }
}
