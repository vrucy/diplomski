using CraftmanPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CraftmanPortal.Data
{
    public class PortalOfCraftsmanDbContext : IdentityDbContext<IdentityUser>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CaseCraftman>()
                .HasKey(bc => new { bc.CaseId, bc.CraftmanId });
            modelBuilder.Entity<CaseCraftman>()
             .HasOne(bc => bc.Craftman)
             .WithMany(b => b.CaseCraftmans)
                 .HasForeignKey(bc => bc.CraftmanId);
            modelBuilder.Entity<CaseCraftman>()
                .HasOne(bc => bc.Case)
                .WithMany(c => c.CaseCraftmans)
                .HasForeignKey(bc => bc.CaseId);
            modelBuilder.Entity<ContractCategory>()
                .HasKey(sm => new { sm.CraftmanId, sm.CategoryId});
            modelBuilder.Entity<ContractCategory>()
                .HasOne(mk => mk.Category)
                .WithMany(m => m.ContractCategories)
                .HasForeignKey(y => y.CategoryId);
            modelBuilder.Entity<ContractCategory>()
                .HasOne(bc => bc.Craftman)
                .WithMany(m => m.Categories)
                .HasForeignKey(y => y.CraftmanId);
            modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Kategorija>().HasData(new Kategorija() { Id = 1, Naziv = "gradjevinarstvo" },
            //                                          new Kategorija() { Id = 2, Naziv = "zidar", ParentId = 1, },
            //                                          new Kategorija() { Id = 3, Naziv = "stolar", ParentId = 1, },
            //                                          new Kategorija() { Id = 4, Naziv = "moler", ParentId = 1, },
            //                                          new Kategorija() { Id = 5, Naziv = "zidar", ParentId = 1, },
            //                                          new Kategorija() { Id = 6, Naziv = "automobilizam", ParentId = null },
            //                                          new Kategorija() { Id = 7, Naziv = "Limar", ParentId = 6, },
            //                                          new Kategorija() { Id = 8, Naziv = "mehianicar", ParentId = 6, });
                                                      
                                                      
                                                                         
                                                      
                                                      

        }
        public PortalOfCraftsmanDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Craftman> Craftmans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<CaseCraftman> CaseCraftmans { get; set; }
        public DbSet<ContractCategory> ContractCategores { get; set;}
        public DbSet<Status> Statuses { get; set; }
        public DbSet<CaseStatus> CaseStatuses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Picture> Pictures { get; set; }

    }
}
