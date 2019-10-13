using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models.View
{
    public class editCenovnikAdvokatVM
    {
        public int Id { get; set; }

        public string vrstaPlacanja { get; set; }
        public string kolicina { get; set; }
        public string komentar { get; set; }
        public bool isKonacan { get; set; }
        public int SlucajId { get; set; }
        public Slucaj Slucaj { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public string IdenityId { get; set; }
        public ApplicationUser Idenity { get; set; }
        public int MajstorId { get; set; }
        public Majstor Majstor { get; set; }
        public DateTime zavrsetakRada { get; set; }
    }
}
