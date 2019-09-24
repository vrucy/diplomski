using AdvokatskiPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdvokatskiPortal.Data
{
    public class PortalAdvokataDbContext : IdentityDbContext<IdentityUser>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SlucajMajstor>()
                .HasKey(bc => new { bc.SlucajId, bc.MajstorId});
            modelBuilder.Entity<SlucajMajstor>()
             .HasOne(bc => bc.Majstor)
             .WithMany(b => b.SlucajMajstors)
                 .HasForeignKey(bc => bc.MajstorId);
            modelBuilder.Entity<SlucajMajstor>()
                .HasOne(bc => bc.Slucaj)
                .WithMany(c => c.SlucajMajstors)
                .HasForeignKey(bc => bc.SlucajId);
        }
            public PortalAdvokataDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Majstor> Majstors  { get; set; }
        public DbSet<Korisnik> Korisniks { get; set; }
        public DbSet<Slucaj> Slucajs{ get; set; }
        public DbSet<Kategorija> Kategorijas{ get; set; }
        public DbSet<PodKategorija> PodKategorijas{ get; set; }
        public DbSet<Cenovnik> Cenovniks{ get; set; }
        public DbSet<SlucajMajstor> SlucajMajstors{ get; set; }
        public DbSet<Status> Statuses{ get; set; }
        public DbSet<Ugovor> Ugovors{ get; set; }
        public DbSet<SlucajStatus> SlucajStatuses { get; set; }
    }
}
