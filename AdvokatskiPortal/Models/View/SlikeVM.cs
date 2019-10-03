using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models.View
{
    public class SlikeVM
    {
        public int Id { get; set; }
        public string imgSource { get; set; }
        public byte[] getSource()
        {
            return Convert.FromBase64String(imgSource);
        }
    }
}
