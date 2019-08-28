using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models.View
{
    public class postSlucajAdvokataSaCenovnikomViewModel
    {
        public Slucaj Slucaj { get; set; }
        public ICollection<Advokat> Advokats { get; set; }
        public string Opis { get; set; }
        public Ugovor Ugovor { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public ICollection<SlucajAdvokat> SlucajAdvokats { get; set; }
        public ICollection<Cenovnik> Cenovniks { get; set; }

    }
}
