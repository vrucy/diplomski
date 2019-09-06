using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class SlucajStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SlucajAdvokat> SlucajAdvokats{ get; set; }
    }
}
