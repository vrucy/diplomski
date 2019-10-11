using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvokatskiPortal.Models
{
    public class SlucajMajstor
    {
        
        public DateTime datumKreiranja { get; set; }
        public DateTime? ZavrsetakRada { get; set; }

        public bool prihvacno { get; set; }
        public string Odgovor { get; set; }
        public bool isRead { get; set; }
        public int MajstorId { get; set; }
        public string MajstorIdStr{ get; set; }
        public Majstor Majstor { get; set; } 
        public int SlucajId { get; set; }
        public Slucaj Slucaj{ get; set; }
        [ForeignKey("SlucajStatus")]
        public int SlucajStatusId { get; set; }
        public SlucajStatus SlucajStatus { get; set; }
    }
}
