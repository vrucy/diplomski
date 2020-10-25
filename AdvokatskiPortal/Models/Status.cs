using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CraftmanPortal.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}
