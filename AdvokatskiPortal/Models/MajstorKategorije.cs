using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class MajstorKategorije
    {
        //public int Id { get; set; }
        [ForeignKey("Majstor")]
        public int MajstorId { get; set; }
        public Majstor  Majstor { get; set; }
        [ForeignKey("Kategorija")]
        public int KategorijaId { get; set; }
        public Kategorija Kategorija{ get; set; }
    }
}
