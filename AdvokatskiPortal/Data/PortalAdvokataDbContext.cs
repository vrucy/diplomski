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
                .HasKey(bc => new { bc.SlucajId, bc.MajstorId });
            modelBuilder.Entity<SlucajMajstor>()
             .HasOne(bc => bc.Majstor)
             .WithMany(b => b.SlucajMajstors)
                 .HasForeignKey(bc => bc.MajstorId);
            modelBuilder.Entity<SlucajMajstor>()
                .HasOne(bc => bc.Slucaj)
                .WithMany(c => c.SlucajMajstors)
                .HasForeignKey(bc => bc.SlucajId);
            //modelBuilder.Entity<MajstorKategorije>()
            //    .HasKey(sm => new { sm.MajstorId, sm.KategorijaId });
            //modelBuilder.Entity<MajstorKategorije>()
            //    .HasOne(mk => mk.Kategorija)
            //    .WithMany(m => m.MajstorKategorijes)
            //    .HasForeignKey(y => y.KategorijaId);
            //modelBuilder.Entity<MajstorKategorije>()
            //    .HasOne(bc => bc.Majstor)
            //    .WithMany(m => m.MajstorKategorijes)
            //    .HasForeignKey(y => y.MajstorId);
            modelBuilder.Entity<Kategorija>().HasOne(x => x.ParentKategorija).WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
        }
        public PortalAdvokataDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Majstor> Majstors { get; set; }
        public DbSet<Korisnik> Korisniks { get; set; }
        public DbSet<Slucaj> Slucajs { get; set; }
        public DbSet<Kategorija> Kategorijas { get; set; }
        public DbSet<Cenovnik> Cenovniks { get; set; }
        public DbSet<SlucajMajstor> SlucajMajstors { get; set; }
        public DbSet<MajstorKategorije> MajstorKategorijes {get; set;}
        public DbSet<Status> Statuses{ get; set; }
        public DbSet<Ugovor> Ugovors{ get; set; }
        public DbSet<SlucajStatus> SlucajStatuses { get; set; }
    }
}
