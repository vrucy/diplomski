using AdvokatskiPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Data
{
    public class PortalAdvokataDbContext : IdentityDbContext<IdentityUser>
    {
        public PortalAdvokataDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Advokat> Advokats { get; set; }
        public DbSet<Korisnik> Korisniks { get; set; }
        public DbSet<Slucaj> Slucajs{ get; set; }

        public DbSet<Cenovnik> Cenovniks{ get; set; }
        public DbSet<SlucajAdvokat> SlucajAdvokats{ get; set; }
        public DbSet<Status> Statuses{ get; set; }
        public DbSet<Ugovor> Ugovors{ get; set; }
    }
}
