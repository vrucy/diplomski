using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class Slucaj
    {
        public int Id { get; set; }
        public string Opis { get; set; }
        public Ugovor Ugovor { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik{ get; set; }
        public ICollection<SlucajAdvokat> SlucajAdvokats{ get; set; }
        public ICollection<Cenovnik> Cenovniks { get; set; }

        //creatorId
        //advokatId
    }
}
