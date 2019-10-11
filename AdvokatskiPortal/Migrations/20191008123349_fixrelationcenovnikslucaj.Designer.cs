﻿// <auto-generated />
using System;
using AdvokatskiPortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdvokatskiPortal.Migrations
{
    [DbContext(typeof(PortalAdvokataDbContext))]
    [Migration("20191008123349_fixrelationcenovnikslucaj")]
    partial class fixrelationcenovnikslucaj
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdvokatskiPortal.Models.Cenovnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IdenityId");

                    b.Property<int>("SlucajId");

                    b.Property<int>("StatusId");

                    b.Property<bool>("isKonacan");

                    b.Property<string>("kolicina");

                    b.Property<string>("komentar");

                    b.Property<string>("vrstaPlacanja");

                    b.HasKey("Id");

                    b.HasIndex("IdenityId");

                    b.HasIndex("SlucajId");

                    b.HasIndex("StatusId");

                    b.ToTable("Cenovniks");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Kategorija", b =>
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

            modelBuilder.Entity("AdvokatskiPortal.Models.Korisnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("IdenityId");

                    b.Property<string>("Ime");

                    b.Property<string>("Mesto");

                    b.Property<string>("Password");

                    b.Property<string>("Prezime");

                    b.Property<string>("Ulica");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("IdenityId");

                    b.ToTable("Korisniks");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Majstor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("IdenityId");

                    b.Property<string>("Ime");

                    b.Property<string>("Mesto");

                    b.Property<string>("Password");

                    b.Property<string>("Prezime");

                    b.Property<string>("Ulica");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("IdenityId");

                    b.ToTable("Majstors");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.MajstorKategorije", b =>
                {
                    b.Property<int>("MajstorId");

                    b.Property<int>("KategorijaId");

                    b.HasKey("MajstorId", "KategorijaId");

                    b.HasIndex("KategorijaId");

                    b.ToTable("MajstorKategorijes");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Slika", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv");

                    b.Property<int>("SlucajId");

                    b.Property<byte[]>("slikaProp");

                    b.HasKey("Id");

                    b.HasIndex("SlucajId");

                    b.ToTable("Slika");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Slucaj", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("GDuzina");

                    b.Property<double>("GSirina");

                    b.Property<int>("KategorijaId");

                    b.Property<int>("KorisnikId");

                    b.Property<DateTime>("KrajnjiRokZaOdgovor");

                    b.Property<string>("Mesto");

                    b.Property<string>("Naziv");

                    b.Property<string>("Opis");

                    b.Property<DateTime?>("PocetakRada");

                    b.Property<string>("UlicaIBroj");

                    b.HasKey("Id");

                    b.HasIndex("KategorijaId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Slucajs");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.SlucajMajstor", b =>
                {
                    b.Property<int>("SlucajId");

                    b.Property<int>("MajstorId");

                    b.Property<string>("MajstorIdStr");

                    b.Property<string>("Odgovor");

                    b.Property<int>("SlucajStatusId");

                    b.Property<DateTime?>("ZavrsetakRada");

                    b.Property<DateTime>("datumKreiranja");

                    b.Property<bool>("isRead");

                    b.Property<bool>("prihvacno");

                    b.HasKey("SlucajId", "MajstorId");

                    b.HasIndex("MajstorId");

                    b.HasIndex("SlucajStatusId");

                    b.ToTable("SlucajMajstors");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.SlucajStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SlucajStatuses");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Ugovor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SlucajId");

                    b.HasKey("Id");

                    b.HasIndex("SlucajId")
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

            modelBuilder.Entity("AdvokatskiPortal.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");


                    b.ToTable("ApplicationUser");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Cenovnik", b =>
                {
                    b.HasOne("AdvokatskiPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");

                    b.HasOne("AdvokatskiPortal.Models.Slucaj", "Slucaj")
                        .WithMany("Cenovniks")
                        .HasForeignKey("SlucajId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AdvokatskiPortal.Models.Status", "Status")
                        .WithMany("Cenovniks")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Kategorija", b =>
                {
                    b.HasOne("AdvokatskiPortal.Models.Kategorija", "ParentKategorija")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Korisnik", b =>
                {
                    b.HasOne("AdvokatskiPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Majstor", b =>
                {
                    b.HasOne("AdvokatskiPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.MajstorKategorije", b =>
                {
                    b.HasOne("AdvokatskiPortal.Models.Kategorija", "Kategorija")
                        .WithMany("MajstorKategorijes")
                        .HasForeignKey("KategorijaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AdvokatskiPortal.Models.Majstor", "Majstor")
                        .WithMany("Kategorije")
                        .HasForeignKey("MajstorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Slika", b =>
                {
                    b.HasOne("AdvokatskiPortal.Models.Slucaj", "Slucaj")
                        .WithMany("Slike")
                        .HasForeignKey("SlucajId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Slucaj", b =>
                {
                    b.HasOne("AdvokatskiPortal.Models.Kategorija", "Kategorija")
                        .WithMany("Slucajs")
                        .HasForeignKey("KategorijaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AdvokatskiPortal.Models.Korisnik", "Korisnik")
                        .WithMany("Slucajs")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.SlucajMajstor", b =>
                {
                    b.HasOne("AdvokatskiPortal.Models.Majstor", "Majstor")
                        .WithMany("SlucajMajstors")
                        .HasForeignKey("MajstorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AdvokatskiPortal.Models.Slucaj", "Slucaj")
                        .WithMany("SlucajMajstors")
                        .HasForeignKey("SlucajId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AdvokatskiPortal.Models.SlucajStatus", "SlucajStatus")
                        .WithMany("SlucajMajstors")
                        .HasForeignKey("SlucajStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AdvokatskiPortal.Models.Ugovor", b =>
                {
                    b.HasOne("AdvokatskiPortal.Models.Slucaj", "Slucaj")
                        .WithOne("Ugovor")
                        .HasForeignKey("AdvokatskiPortal.Models.Ugovor", "SlucajId")
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
