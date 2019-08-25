using AdvokatskiPortal.Data;
using AdvokatskiPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Repository
{
    public class AdvokatRepo : IAdvokatRepo
    {
        private PortalAdvokataDbContext _context;

        public AdvokatRepo(PortalAdvokataDbContext context)
        {
            _context = context;
        }

        public Advokat GetAdvokat(string id)
        {
            return _context.Advokats.Single(x => x.Idenity.Id == id);
        }

        public Advokat getAdvokatById(int id)
        {
            return _context.Advokats.Single(x => x.Id == id);
        }

        public IEnumerable<Advokat> GetAll()
        {
            return _context.Advokats;
        }
    }
}
