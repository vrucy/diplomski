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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SlucajAdvokat>()
                .HasKey(bc => new { bc.SlucajId, bc.AdvokatId});
            modelBuilder.Entity<SlucajAdvokat>()
             .HasOne(bc => bc.Advokat)
             .WithMany(b => b.SlucajAdvokats)
                 .HasForeignKey(bc => bc.AdvokatId);
            modelBuilder.Entity<SlucajAdvokat>()
                .HasOne(bc => bc.Slucaj)
                .WithMany(c => c.SlucajAdvokats)
                .HasForeignKey(bc => bc.SlucajId);
        }
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
