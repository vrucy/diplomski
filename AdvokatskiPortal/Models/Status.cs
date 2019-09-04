using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Cenovnik> Cenovniks { get; set; }
    }
}
