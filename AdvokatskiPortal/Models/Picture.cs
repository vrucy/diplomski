using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CraftmanPortal.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte [] PictureBytes { get; set; }
        public int CaseId { get; set; }
        public Case Case { get; set; }
    }

}
