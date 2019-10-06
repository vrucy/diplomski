using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvokatskiPortal.Models
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
        //public ICollection<Majstor> Majstors { get; set; }
        [ForeignKey("Majstor")]
        public int? MajstorId { get; set; }
        public Majstor Majstor { get; set; }
        public ICollection<MajstorKategorije> MajstorKategorijes { get; set; }
        public ICollection<Slucaj> Slucajs { get; set; }
    }
}
