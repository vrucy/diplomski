using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public ApplicationUser Idenity { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mesto { get; set; }
        public string Ulica { get; set; }
        public ICollection<Slucaj>Slucajs { get; set; }

    }
}
