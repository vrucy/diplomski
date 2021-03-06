﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class Slucaj
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double GSirina { get; set; }
        public double GDuzina { get; set; }
        public string Mesto { get; set; }
        public string UlicaIBroj { get; set; }
        public DateTime KrajnjiRokZaOdgovor { get; set; }
        public DateTime? PocetakRada{ get; set; }
        public DateTime? ZavrsetakRada { get; set; }
        public Ugovor Ugovor { get; set; }
        public int KategorijaId { get; set; }
        public Kategorija Kategorija { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik{ get; set; }
        public ICollection<Slika> Slike { get; set; }
        public ICollection<SlucajMajstor> SlucajMajstors{ get; set; }
        public ICollection<Cenovnik> Cenovniks { get; set; }
    }
}
