﻿// <auto-generated />
using System;
using BikeKinnus.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BikeKinnus.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241226081529_CompanySeeded")]
    partial class CompanySeeded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BikeKinnus.Models.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "High-performance bikes designed for speed and agility.",
                            Name = "Sports"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Comfortable bikes designed for long-distance cruising.",
                            Name = "Cruiser"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Affordable and fuel-efficient bikes for daily commuting.",
                            Name = "Commuter"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Robust bikes built for both off-road and on-road riding.",
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Bikes equipped for extended journeys with enhanced comfort.",
                            Name = "Touring"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Street bikes with minimal fairing for an urban look.",
                            Name = "Naked"
                        });
                });

            modelBuilder.Entity("BikeKinnus.Models.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Shanghai",
                            Name = "Tech Solution",
                            PhoneNumber = "6669990000",
                            PostalCode = "12121",
                            State = "IL",
                            StreetAddress = "123 Tech St"
                        },
                        new
                        {
                            Id = 2,
                            City = "Shenzen",
                            Name = "Vivid Books",
                            PhoneNumber = "7779990000",
                            PostalCode = "66666",
                            State = "IL",
                            StreetAddress = "999 Vid St"
                        },
                        new
                        {
                            Id = 3,
                            City = "GHuangzhou",
                            Name = "Readers Club",
                            PhoneNumber = "1113335555",
                            PostalCode = "99999",
                            State = "NY",
                            StreetAddress = "999 Main St"
                        });
                });

            modelBuilder.Entity("BikeKinnus.Models.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EngineCapacity")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Mileage")
                        .HasColumnType("float");

                    b.Property<int>("ModelYear")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Yamaha",
                            CategoryId = 1,
                            Description = "155cc fuel-injected engine with aggressive styling and efficient mileage.",
                            EngineCapacity = 155,
                            ImageUrl = "/images/yamaha_fz_v3.jpg",
                            Mileage = 45.0,
                            ModelYear = 2023,
                            Price = 390000.0,
                            Title = "Yamaha FZ V3"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Bajaj",
                            CategoryId = 2,
                            Description = "199.5cc liquid-cooled engine with sharp design and sporty performance.",
                            EngineCapacity = 199,
                            ImageUrl = "/images/pulsar_ns200.jpg",
                            Mileage = 35.0,
                            ModelYear = 2023,
                            Price = 374000.0,
                            Title = "Pulsar NS200"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Bajaj",
                            CategoryId = 4,
                            Description = "373cc sports-touring beast with premium performance and LED headlamps.",
                            EngineCapacity = 373,
                            ImageUrl = "/images/dominar_400.jpg",
                            Mileage = 30.0,
                            ModelYear = 2023,
                            Price = 669500.0,
                            Title = "Dominar 400"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Suzuki",
                            CategoryId = 2,
                            Description = "Sporty 155cc engine with aggressive styling and aerodynamic design.",
                            EngineCapacity = 155,
                            ImageUrl = "/images/suzuki_gixxer_160.jpg",
                            Mileage = 45.0,
                            ModelYear = 2023,
                            Price = 278000.0,
                            Title = "Suzuki Gixxer 160"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Bajaj",
                            CategoryId = 3,
                            Description = "160cc engine offering relaxed ergonomics for long-distance cruising.",
                            EngineCapacity = 160,
                            ImageUrl = "/images/avenger_160_cruise.jpg",
                            Mileage = 45.0,
                            ModelYear = 2022,
                            Price = 214000.0,
                            Title = "Avenger 160 Cruise"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "Bajaj",
                            CategoryId = 5,
                            Description = "A tribute to the INS Vikrant with a 149cc engine for efficient performance.",
                            EngineCapacity = 149,
                            ImageUrl = "/images/vikrant_ins150.jpg",
                            Mileage = 55.0,
                            ModelYear = 2021,
                            Price = 195000.0,
                            Title = "Vikrant INS 150"
                        },
                        new
                        {
                            Id = 7,
                            Brand = "TVS",
                            CategoryId = 5,
                            Description = "160cc race-tuned fuel-injected engine with sporty graphics.",
                            EngineCapacity = 160,
                            ImageUrl = "/images/apache_rtr160.jpg",
                            Mileage = 50.0,
                            ModelYear = 2022,
                            Price = 248000.0,
                            Title = "Apache RTR 160 4V"
                        },
                        new
                        {
                            Id = 8,
                            Brand = "KTM",
                            CategoryId = 1,
                            Description = "The KTM Duke 200 is a high-performance street motorcycle with a 199.5cc engine, offering impressive power and stylish design for enthusiasts.",
                            EngineCapacity = 199,
                            ImageUrl = "/images/ktm_duke_200.jpg",
                            Mileage = 35.0,
                            ModelYear = 2024,
                            Price = 558000.0,
                            Title = "KTM Duke 200"
                        },
                        new
                        {
                            Id = 9,
                            Brand = "Bajaj",
                            CategoryId = 5,
                            Description = "Classic 149cc DTS-i engine offering power and efficiency.",
                            EngineCapacity = 149,
                            ImageUrl = "/images/pulsar_150.jpg",
                            Mileage = 55.0,
                            ModelYear = 2022,
                            Price = 243000.0,
                            Title = "Pulsar 150"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BikeKinnus.Models.Models.AppUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("AppUser");
                });

            modelBuilder.Entity("BikeKinnus.Models.Models.Product", b =>
                {
                    b.HasOne("BikeKinnus.Models.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
