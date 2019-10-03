using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class Slika
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public byte [] slikaProp { get; set; }
        public int SlucajId { get; set; }
        public Slucaj Slucaj { get; set; }
    }

}
