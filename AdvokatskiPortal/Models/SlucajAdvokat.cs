using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class SlucajAdvokat
    {
        public int Id { get; set; }
        
        public DateTime datumKreiranja { get; set; }
        public bool prihvacno { get; set; }
        public string Odgovor { get; set; }
        public int AdvokatId { get; set; }
        public Advokat Advokat{ get; set; }
        public int SlucajId { get; set; }

        public Slucaj Slucaj{ get; set; }
    }
}
