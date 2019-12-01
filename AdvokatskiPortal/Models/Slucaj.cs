using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MajstorskiPortal.Models
{
    public class Slucaj
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double GSirina { get; set; }
        public double GDuzina { get; set; }
        public DateTime KrajnjiRokZaOdgovor { get; set; }
        //public DateTime? PocetakRada{ get; set; }
        //public DateTime zavrsetakRada { get; set; }

        //public Ugovor Ugovor { get; set; }
        public int KategorijaId { get; set; }
        public Kategorija Kategorija { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik{ get; set; }
        public ICollection<Slika> Slike { get; set; }
        public ICollection<SlucajMajstor> SlucajMajstors{ get; set; }
        //public int CenovnikId { get; set; }
        //public Cenovnik Cenovnik { get; set; }
        public ICollection<Cenovnik> Cenovniks{ get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
