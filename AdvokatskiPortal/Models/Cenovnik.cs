using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class Cenovnik
    {
        public int Id { get; set; }
        //slucajId
        //userId
        public string vrstaPlacanja { get; set; }
        public string kolicina { get; set; }
        //public ICollection<Slucaj> Slucajs { get; set; }
        public int SlucajId { get; set; }
        public Slucaj Slucaj { get; set; }

        [ForeignKey("Status")]
        public int StatusId{ get; set; }
        public Status Status { get; set; }
        //statusId
    }
}
