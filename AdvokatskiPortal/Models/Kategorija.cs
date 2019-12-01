using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajstorskiPortal.Models
{
    public class Kategorija
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        [ForeignKey("ParentKategorija")]
        public int? ParentId { get; set; }
        public Kategorija ParentKategorija { get; set; }
        
        public ICollection<MajstorKategorije> MajstorKategorijes { get; set; }
        public ICollection<Slucaj> Slucajs { get; set; }
    }
}
