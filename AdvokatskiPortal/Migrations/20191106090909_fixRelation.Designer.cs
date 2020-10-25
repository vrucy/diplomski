﻿// <auto-generated />
using System;
using CraftmanPortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CraftmanPortal.Migrations
{
    [DbContext(typeof(PortalOfCraftsmanDbContext))]
    [Migration("20191106090909_fixRelation")]
    partial class fixRelation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CraftmanPortal.Models.Cenovnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IdenityId");

                    b.Property<int>("CraftmanId");

                    b.Property<DateTime?>("PocetakRada");

                    b.Property<int>("CaseId");

                    b.Property<int>("StatusId");

                    b.Property<bool>("isKonacan");

                    b.Property<string>("kolicina");

                    b.Property<string>("komentar");

                    b.Property<string>("vrstaPlacanja");

                    b.Property<DateTime?>("zavrsetakRada");

                    b.HasKey("Id");

                    b.HasIndex("IdenityId");

                    b.HasIndex("CraftmanId");

                    b.HasIndex("CaseId");

                    b.HasIndex("StatusId");

                    b.ToTable("Cenovniks");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Kategorija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv");

                    b.Property<string>("Opis");

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Kategorijas");
                });

            modelBuilder.Entity("CraftmanPortal.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("IdenityId");

                    b.Property<string>("FirstName");

                    b.Property<string>("Mesto");

                    b.Property<string>("Password");

                    b.Property<string>("PrezFirstName");

                    b.Property<string>("Ulica");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("IdenityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Craftman", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("IdenityId");

                    b.Property<string>("FirstName");

                    b.Property<string>("Mesto");

                    b.Property<string>("Password");

                    b.Property<string>("PrezFirstName");

                    b.Property<string>("Ulica");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("IdenityId");

                    b.ToTable("Craftmans");
                });

            modelBuilder.Entity("CraftmanPortal.Models.CraftmanKategorije", b =>
                {
                    b.Property<int>("CraftmanId");

                    b.Property<int>("KategorijaId");

                    b.HasKey("CraftmanId", "KategorijaId");

                    b.HasIndex("KategorijaId");

                    b.ToTable("CraftmanKategorijes");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NotificationText");

                    b.Property<int>("CaseId");

                    b.Property<DateTime>("TFirstNameStamp");

                    b.Property<string>("UserId");

                    b.Property<bool>("isRead");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Slika", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv");

                    b.Property<int>("CaseId");

                    b.Property<byte[]>("slikaProp");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Slikas");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("GDuzina");

                    b.Property<double>("GSirina");

                    b.Property<int>("KategorijaId");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("KrajnjiRokZaOdgovor");

                    b.Property<string>("Mesto");

                    b.Property<string>("Naziv");

                    b.Property<string>("Opis");

                    b.Property<string>("UlicaIBroj");

                    b.HasKey("Id");

                    b.HasIndex("KategorijaId");

                    b.HasIndex("UserId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("CraftmanPortal.Models.CaseCraftman", b =>
                {
                    b.Property<int>("CaseId");

                    b.Property<int>("CraftmanId");

                    b.Property<string>("CraftmanIdStr");

                    b.Property<string>("Odgovor");

                    b.Property<int>("CaseStatusId");

                    b.Property<DateTime>("datumKreiranja");

                    b.Property<bool>("isRead");

                    b.Property<bool>("isReadOdbijenCraftman");

                    b.Property<bool>("isReadOdbijenUser");

                    b.Property<bool>("isReject");

                    b.Property<bool>("prihvacno");

                    b.HasKey("CaseId", "CraftmanId");

                    b.HasIndex("CraftmanId");

                    b.HasIndex("CaseStatusId");

                    b.ToTable("CaseCraftmans");
                });

            modelBuilder.Entity("CraftmanPortal.Models.CaseStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CaseStatuses");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Ugovor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseId");

                    b.HasKey("Id");

                    b.HasIndex("CaseId")
                        .IsUnique();

                    b.ToTable("Ugovors");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CraftmanPortal.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");


                    b.ToTable("ApplicationUser");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Cenovnik", b =>
                {
                    b.HasOne("CraftmanPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");

                    b.HasOne("CraftmanPortal.Models.Craftman", "Craftman")
                        .WithMany("Cenovniks")
                        .HasForeignKey("CraftmanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.Case", "Case")
                        .WithMany("Cenovniks")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.Status", "Status")
                        .WithMany("Cenovniks")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.Kategorija", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Kategorija", "ParentKategorija")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CraftmanPortal.Models.User", b =>
                {
                    b.HasOne("CraftmanPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Craftman", b =>
                {
                    b.HasOne("CraftmanPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");
                });

            modelBuilder.Entity("CraftmanPortal.Models.CraftmanKategorije", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Kategorija", "Kategorija")
                        .WithMany("CraftmanKategorijes")
                        .HasForeignKey("KategorijaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.Craftman", "Craftman")
                        .WithMany("Kategorije")
                        .HasForeignKey("CraftmanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.Notification", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Case", "Case")
                        .WithMany("Notifications")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.Slika", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Case", "Case")
                        .WithMany("Slike")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.Case", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Kategorija", "Kategorija")
                        .WithMany("Cases")
                        .HasForeignKey("KategorijaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.User", "User")
                        .WithMany("Cases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.CaseCraftman", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Craftman", "Craftman")
                        .WithMany("CaseCraftmans")
                        .HasForeignKey("CraftmanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.Case", "Case")
                        .WithMany("CaseCraftmans")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.CaseStatus", "CaseStatus")
                        .WithMany("CaseCraftmans")
                        .HasForeignKey("CaseStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.Ugovor", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Case", "Case")
                        .WithOne("Ugovor")
                        .HasForeignKey("CraftmanPortal.Models.Ugovor", "CaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
