using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class Specjalnost
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public ICollection<SpecjalnostiAdvokat> specjalnostiAdvokats { get; set; }

    }
}
