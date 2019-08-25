using AdvokatskiPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Repository
{
    public interface IAdvokatRepo
    {
        Advokat GetAdvokat(string id);
        IEnumerable<Advokat> GetAll();
        Advokat getAdvokatById(int id);
    }
}
