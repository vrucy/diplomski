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
    [Migration("20201025093932_addColumnToContractIsFinal")]
    partial class addColumnToContractIsFinal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CraftmanPortal.Models.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("DeadLineForAnswer");

                    b.Property<string>("Description");

                    b.Property<double>("GDuzina");

                    b.Property<double>("GSirina");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("CraftmanPortal.Models.CaseCraftman", b =>
                {
                    b.Property<int>("CaseId");

                    b.Property<int>("CraftmanId");

                    b.Property<int>("CaseStatusId");

                    b.Property<string>("CraftmanIdIndentity");

                    b.Property<DateTime>("CreationDate");

                    b.HasKey("CaseId", "CraftmanId");

                    b.HasIndex("CaseStatusId");

                    b.HasIndex("CraftmanId");

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

            modelBuilder.Entity("CraftmanPortal.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseId");

                    b.Property<DateTime?>("ChangeCaseDate");

                    b.Property<string>("Comment");

                    b.Property<int>("CraftmanId");

                    b.Property<DateTime?>("FinishDate");

                    b.Property<string>("Price");

                    b.Property<DateTime?>("ReciveCase");

                    b.Property<DateTime?>("StartDate");

                    b.Property<int>("StatusId");

                    b.Property<string>("TypeOfPayment");

                    b.Property<bool>("isFinal");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.HasIndex("CraftmanId");

                    b.HasIndex("StatusId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("CraftmanPortal.Models.ContractCategory", b =>
                {
                    b.Property<int>("CraftmanId");

                    b.Property<int>("CategoryId");

                    b.HasKey("CraftmanId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ContractCategores");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Craftman", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("IdenityId");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Place");

                    b.Property<string>("Street");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("IdenityId");

                    b.ToTable("Craftmans");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseId");

                    b.Property<string>("NotificationText");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("UserId");

                    b.Property<bool>("isRead");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseId");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PictureBytes");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Pictures");
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

            modelBuilder.Entity("CraftmanPortal.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("IdenityId");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Place");

                    b.Property<string>("Street");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("IdenityId");

                    b.ToTable("Users");
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

            modelBuilder.Entity("CraftmanPortal.Models.Case", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Category", "Category")
                        .WithMany("Cases")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.User", "User")
                        .WithMany("Cases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.CaseCraftman", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Case", "Case")
                        .WithMany("CaseCraftmans")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.CaseStatus", "CaseStatus")
                        .WithMany("CaseCraftmans")
                        .HasForeignKey("CaseStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.Craftman", "Craftman")
                        .WithMany("CaseCraftmans")
                        .HasForeignKey("CraftmanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.Category", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CraftmanPortal.Models.Contract", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Case", "Case")
                        .WithMany("Contracts")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.Craftman", "Craftman")
                        .WithMany("Contracts")
                        .HasForeignKey("CraftmanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.Status", "Status")
                        .WithMany("Contracts")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.ContractCategory", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Category", "Category")
                        .WithMany("ContractCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CraftmanPortal.Models.Craftman", "Craftman")
                        .WithMany("Categories")
                        .HasForeignKey("CraftmanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.Craftman", b =>
                {
                    b.HasOne("CraftmanPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");
                });

            modelBuilder.Entity("CraftmanPortal.Models.Notification", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Case", "Case")
                        .WithMany("Notifications")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.Picture", b =>
                {
                    b.HasOne("CraftmanPortal.Models.Case", "Case")
                        .WithMany("Pictures")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CraftmanPortal.Models.User", b =>
                {
                    b.HasOne("CraftmanPortal.Models.ApplicationUser", "Idenity")
                        .WithMany()
                        .HasForeignKey("IdenityId");
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
