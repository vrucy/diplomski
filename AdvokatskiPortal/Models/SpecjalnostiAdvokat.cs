using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class SpecjalnostiAdvokat
    {
        public int Id { get; set; }
        public int SpecjalnostId { get; set; }
        public Specjalnost Specjalnost { get; set; }
        public int AdvokatId { get; set; }
        public Advokat Advokats { get; set; }
    }
}
