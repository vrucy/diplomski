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
        
        public string vrstaPlacanja { get; set; }
        public string kolicina { get; set; }
        public string komentar { get; set; }
        public bool isKonacan { get; set; }
        public DateTime? PocetakRada { get; set; }
        public DateTime? zavrsetakRada { get; set; }
        [ForeignKey("Slucaj")]
        public int SlucajId { get; set; }
        public Slucaj Slucaj { get; set; }
        public int StatusId{ get; set; }
        public Status Status { get; set; }
        public string IdenityId { get; set; }
        public ApplicationUser Idenity { get; set; }
        [ForeignKey("Majstor")]
        public int MajstorId { get; set; }
        public Majstor Majstor { get; set; }

        //statusId
    }
}
