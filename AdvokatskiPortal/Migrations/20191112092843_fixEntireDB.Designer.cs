﻿// <auto-generated />
using System;
using MajstorskiPortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MajstorskiPortal.Migrations
{
    [DbContext(typeof(PortalMajstoraDbContext))]
    [Migration("20191112092843_fixEntireDB")]
    partial class fixEntireDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MajstorskiPortal.Models.Cenovnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IdenityId");

                    b.Property<int>("MajstorId");

                    b.Property<DateTime?>("PocetakRada");

                    b.Property<int>("SlucajId");

                    b.Property<int>("StatusId");

                    b.Property<bool>("isKonacan");

                    b.Property<string>("kolicina");

                    b.Property<string>("komentar");

                    b.Property<string>("vrstaPlacanja");

                    b.Property<DateTime?>("zavrsetakRada");

                    b.HasKey("Id");

                    b.HasIndex("MajstorId");

                    b.HasIndex("SlucajId");

                    b.HasIndex("StatusId");

                    b.ToTable("Cenovniks");
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Kategorija", b =>
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

            modelBuilder.Entity("MajstorskiPortal.Models.Korisnik", b =>
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

            modelBuilder.Entity("MajstorskiPortal.Models.Majstor", b =>
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

            modelBuilder.Entity("MajstorskiPortal.Models.MajstorKategorije", b =>
                {
                    b.Property<int>("MajstorId");

                    b.Property<int>("KategorijaId");

                    b.HasKey("MajstorId", "KategorijaId");

                    b.HasIndex("KategorijaId");

                    b.ToTable("MajstorKategorijes");
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NotificationText");

                    b.Property<int>("SlucajId");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("UserId");

                    b.Property<bool>("isRead");

                    b.HasKey("Id");

                    b.HasIndex("SlucajId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Slika", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv");

                    b.Property<int>("SlucajId");

                    b.Property<byte[]>("slikaProp");

                    b.HasKey("Id");

                    b.HasIndex("SlucajId");

                    b.ToTable("Slikas");
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Slucaj", b =>
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

                    b.Property<string>("UlicaIBroj");

                    b.HasKey("Id");

                    b.HasIndex("KategorijaId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Slucajs");
                });

            modelBuilder.Entity("MajstorskiPortal.Models.SlucajMajstor", b =>
                {
                    b.Property<int>("SlucajId");

                    b.Property<int>("MajstorId");

                    b.Property<string>("MajstorIdStr");

                    b.Property<string>("Odgovor");

                    b.Property<int>("SlucajStatusId");

                    b.Property<DateTime>("datumKreiranja");

                    b.Property<bool>("isReject");

                    b.HasKey("SlucajId", "MajstorId");

                    b.HasIndex("MajstorId");

                    b.HasIndex("SlucajStatusId");

                    b.ToTable("SlucajMajstors");
                });

            modelBuilder.Entity("MajstorskiPortal.Models.SlucajStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SlucajStatuses");
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
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

            modelBuilder.Entity("MajstorskiPortal.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");


                    b.ToTable("ApplicationUser");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Cenovnik", b =>
                {
                    b.HasOne("MajstorskiPortal.Models.Majstor", "Majstor")
                        .WithMany("Cenovniks")
                        .HasForeignKey("MajstorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MajstorskiPortal.Models.Slucaj", "Slucaj")
                        .WithMany("Cenovniks")
                        .HasForeignKey("SlucajId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MajstorskiPortal.Models.Status", "Status")
                        .WithMany("Cenovniks")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Kategorija", b =>
                {
                    b.HasOne("MajstorskiPortal.Models.Kategorija", "ParentKategorija")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Korisnik", b =>
                {
                    b.HasOne("MajstorskiPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Majstor", b =>
                {
                    b.HasOne("MajstorskiPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");
                });

            modelBuilder.Entity("MajstorskiPortal.Models.MajstorKategorije", b =>
                {
                    b.HasOne("MajstorskiPortal.Models.Kategorija", "Kategorija")
                        .WithMany("MajstorKategorijes")
                        .HasForeignKey("KategorijaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MajstorskiPortal.Models.Majstor", "Majstor")
                        .WithMany("Kategorije")
                        .HasForeignKey("MajstorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Notification", b =>
                {
                    b.HasOne("MajstorskiPortal.Models.Slucaj", "Slucaj")
                        .WithMany("Notifications")
                        .HasForeignKey("SlucajId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Slika", b =>
                {
                    b.HasOne("MajstorskiPortal.Models.Slucaj", "Slucaj")
                        .WithMany("Slike")
                        .HasForeignKey("SlucajId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MajstorskiPortal.Models.Slucaj", b =>
                {
                    b.HasOne("MajstorskiPortal.Models.Kategorija", "Kategorija")
                        .WithMany("Slucajs")
                        .HasForeignKey("KategorijaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MajstorskiPortal.Models.Korisnik", "Korisnik")
                        .WithMany("Slucajs")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MajstorskiPortal.Models.SlucajMajstor", b =>
                {
                    b.HasOne("MajstorskiPortal.Models.Majstor", "Majstor")
                        .WithMany("SlucajMajstors")
                        .HasForeignKey("MajstorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MajstorskiPortal.Models.Slucaj", "Slucaj")
                        .WithMany("SlucajMajstors")
                        .HasForeignKey("SlucajId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MajstorskiPortal.Models.SlucajStatus", "SlucajStatus")
                        .WithMany("SlucajMajstors")
                        .HasForeignKey("SlucajStatusId")
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
