﻿// <auto-generated />
using System;
using ContractorskiPortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContractorskiPortal.Migrations
{
    [DbContext(typeof(PortalOfCraftsmanDbContext))]
    [Migration("20191005161215_fixContractorKategorije")]
    partial class fixContractorKategorije
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContractorskiPortal.Models.Cenovnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IdenityId");

                    b.Property<int>("CaseId");

                    b.Property<int>("StatusId");

                    b.Property<bool>("isKonacan");

                    b.Property<string>("kolicina");

                    b.Property<string>("komentar");

                    b.Property<string>("vrstaPlacanja");

                    b.HasKey("Id");

                    b.HasIndex("IdenityId");

                    b.HasIndex("CaseId")
                        .IsUnique();

                    b.HasIndex("StatusId");

                    b.ToTable("Cenovniks");
                });

            modelBuilder.Entity("ContractorskiPortal.Models.Kategorija", b =>
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

            modelBuilder.Entity("ContractorskiPortal.Models.User", b =>
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

            modelBuilder.Entity("ContractorskiPortal.Models.Contractor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("IdenityId");

                    b.Property<string>("FirstName");

                    b.Property<int>("KategorijaId");

                    b.Property<string>("Mesto");

                    b.Property<string>("Password");

                    b.Property<string>("PrezFirstName");

                    b.Property<string>("Ulica");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("IdenityId");

                    b.HasIndex("KategorijaId");

                    b.ToTable("Contractors");
                });

            modelBuilder.Entity("ContractorskiPortal.Models.ContractorKategorije", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KategorijaId");

                    b.Property<int>("ContractorId");

                    b.HasKey("Id");

                    b.HasIndex("KategorijaId");

                    b.HasIndex("ContractorId");

                    b.ToTable("ContractorKategorijes");
                });

            modelBuilder.Entity("ContractorskiPortal.Models.Slika", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv");

                    b.Property<int>("CaseId");

                    b.Property<byte[]>("slikaProp");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Slika");
                });

            modelBuilder.Entity("ContractorskiPortal.Models.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CenovnikId");

                    b.Property<double>("GDuzina");

                    b.Property<double>("GSirina");

                    b.Property<int>("KategorijaId");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("KrajnjiRokZaOdgovor");

                    b.Property<string>("Mesto");

                    b.Property<string>("Naziv");

                    b.Property<string>("Opis");

                    b.Property<DateTime?>("PocetakRada");

                    b.Property<string>("UlicaIBroj");

                    b.HasKey("Id");

                    b.HasIndex("KategorijaId");

                    b.HasIndex("UserId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("ContractorskiPortal.Models.CaseContractor", b =>
                {
                    b.Property<int>("CaseId");

                    b.Property<int>("ContractorId");

                    b.Property<string>("Odgovor");

                    b.Property<int>("CaseStatusId");

                    b.Property<DateTime?>("ZavrsetakRada");

                    b.Property<DateTime>("datumKreiranja");

                    b.Property<bool>("isRead");

                    b.Property<bool>("prihvacno");

                    b.HasKey("CaseId", "ContractorId");

                    b.HasIndex("ContractorId");

                    b.HasIndex("CaseStatusId");

                    b.ToTable("CaseContractors");
                });

            modelBuilder.Entity("ContractorskiPortal.Models.CaseStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CaseStatuses");
                });

            modelBuilder.Entity("ContractorskiPortal.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("ContractorskiPortal.Models.Ugovor", b =>
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

            modelBuilder.Entity("ContractorskiPortal.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");


                    b.ToTable("ApplicationUser");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("ContractorskiPortal.Models.Cenovnik", b =>
                {
                    b.HasOne("ContractorskiPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");

                    b.HasOne("ContractorskiPortal.Models.Case", "Case")
                        .WithOne("Cenovnik")
                        .HasForeignKey("ContractorskiPortal.Models.Cenovnik", "CaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContractorskiPortal.Models.Status", "Status")
                        .WithMany("Cenovniks")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContractorskiPortal.Models.Kategorija", b =>
                {
                    b.HasOne("ContractorskiPortal.Models.Kategorija", "ParentKategorija")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ContractorskiPortal.Models.User", b =>
                {
                    b.HasOne("ContractorskiPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");
                });

            modelBuilder.Entity("ContractorskiPortal.Models.Contractor", b =>
                {
                    b.HasOne("ContractorskiPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");

                    b.HasOne("ContractorskiPortal.Models.Kategorija", "Kategorija")
                        .WithMany("Contractors")
                        .HasForeignKey("KategorijaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContractorskiPortal.Models.ContractorKategorije", b =>
                {
                    b.HasOne("ContractorskiPortal.Models.Kategorija", "Kategorija")
                        .WithMany("ContractorKategorijes")
                        .HasForeignKey("KategorijaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContractorskiPortal.Models.Contractor", "Contractor")
                        .WithMany()
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContractorskiPortal.Models.Slika", b =>
                {
                    b.HasOne("ContractorskiPortal.Models.Case", "Case")
                        .WithMany("Slike")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContractorskiPortal.Models.Case", b =>
                {
                    b.HasOne("ContractorskiPortal.Models.Kategorija", "Kategorija")
                        .WithMany("Cases")
                        .HasForeignKey("KategorijaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContractorskiPortal.Models.User", "User")
                        .WithMany("Cases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContractorskiPortal.Models.CaseContractor", b =>
                {
                    b.HasOne("ContractorskiPortal.Models.Contractor", "Contractor")
                        .WithMany("CaseContractors")
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContractorskiPortal.Models.Case", "Case")
                        .WithMany("CaseContractors")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContractorskiPortal.Models.CaseStatus", "CaseStatus")
                        .WithMany("CaseContractors")
                        .HasForeignKey("CaseStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContractorskiPortal.Models.Ugovor", b =>
                {
                    b.HasOne("ContractorskiPortal.Models.Case", "Case")
                        .WithOne("Ugovor")
                        .HasForeignKey("ContractorskiPortal.Models.Ugovor", "CaseId")
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
