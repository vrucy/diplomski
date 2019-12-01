using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajstorskiPortal.Models
{
    public class SlucajStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SlucajMajstor> SlucajMajstors { get; set; }
    }
}
