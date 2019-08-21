using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class Ugovor
    {
        public int Id { get; set; }

        [ForeignKey("Slucaj")]
        public int SlucajId { get; set; }
        public Slucaj Slucaj { get; set; }
        //slucajId
    }
}
