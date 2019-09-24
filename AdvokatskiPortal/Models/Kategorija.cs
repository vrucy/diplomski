using System.Collections.Generic;

namespace AdvokatskiPortal.Models
{
    public class Kategorija
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int PodKategorijaId { get; set; }
        public PodKategorija PodKategorija { get; set; }
        public ICollection<Slucaj> Slucajs { get; set; }
    }
}
