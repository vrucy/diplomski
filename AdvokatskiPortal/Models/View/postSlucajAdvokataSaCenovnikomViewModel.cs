using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models.View
{
    public class postSlucajAdvokataSaCenovnikomViewModel
    {
        public Slucaj Slucaj { get; set; }
        public ICollection<Majstor> Majstors { get; set; }
        public string Opis { get; set; }
        public Ugovor Ugovor { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public ICollection<SlucajMajstor> SlucajMajstors { get; set; }
        public ICollection<Cenovnik> Cenovniks { get; set; }

    }
}
