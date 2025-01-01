using BikeKinnus.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeKinnus.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public  DbSet<Company> Companies { get; set; }
        public DbSet<BuyingCart> BuyingCarts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                 new Category { Id = 1, Name = "Sports", Description = "High-performance bikes designed for speed and agility." },
    new Category { Id = 2, Name = "Cruiser", Description = "Comfortable bikes designed for long-distance cruising." },
    new Category { Id = 3, Name = "Commuter", Description = "Affordable and fuel-efficient bikes for daily commuting." },
    new Category { Id = 4, Name = "Adventure", Description = "Robust bikes built for both off-road and on-road riding." },
    new Category { Id = 5, Name = "Touring", Description = "Bikes equipped for extended journeys with enhanced comfort." },
    new Category { Id = 6, Name = "Naked", Description = "Street bikes with minimal fairing for an urban look." }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Yamaha FZ V3",
                    Description = "155cc fuel-injected engine with aggressive styling and efficient mileage.",
                    Brand = "Yamaha",
                    Price = 390000,
                    ModelYear = 2023,
                    EngineCapacity = 155,
                    Mileage = 45,
                    CategoryId = 1,
                    ImageUrl = "/images/yamaha_fz_v3.jpg"
                },
                new Product
                {
                    Id = 2,
                    Title = "Pulsar NS200",
                    Description = "199.5cc liquid-cooled engine with sharp design and sporty performance.",
                    Brand = "Bajaj",
                    Price = 374000,
                    ModelYear = 2023,
                    EngineCapacity = 199,
                    Mileage = 35,
                    CategoryId = 2,
                    ImageUrl = "/images/pulsar_ns200.jpg"
                },
               
               
                new Product
                {
                    Id = 3,
                    Title = "Dominar 400",
                    Description = "373cc sports-touring beast with premium performance and LED headlamps.",
                    Brand = "Bajaj",
                    Price = 669500,
                    ModelYear = 2023,
                    EngineCapacity = 373,
                    Mileage = 30,
                    CategoryId = 4,
                    ImageUrl = "/images/dominar_400.jpg"
                },
                new Product
                {
                    Id = 4,
                    Title = "Suzuki Gixxer 160",
                    Description = "Sporty 155cc engine with aggressive styling and aerodynamic design.",
                    Brand = "Suzuki",
                    Price = 278000,
                    ModelYear = 2023,
                    EngineCapacity = 155,
                    Mileage = 45,
                    CategoryId = 2,
                    ImageUrl = "/images/suzuki_gixxer_160.jpg"
                },
                new Product
                {
                    Id = 5,
                    Title = "Avenger 160 Cruise",
                    Description = "160cc engine offering relaxed ergonomics for long-distance cruising.",
                    Brand = "Bajaj",
                    Price = 214000,
                    ModelYear = 2022,
                    EngineCapacity = 160,
                    Mileage = 45,
                    CategoryId = 3,
                    ImageUrl = "/images/avenger_160_cruise.jpg"
                },
                new Product
                {
                    Id = 6,
                    Title = "Vikrant INS 150",
                    Description = "A tribute to the INS Vikrant with a 149cc engine for efficient performance.",
                    Brand = "Bajaj",
                    Price = 195000,
                    ModelYear = 2021,
                    EngineCapacity = 149,
                    Mileage = 55,
                    CategoryId = 5,
                    ImageUrl = "/images/vikrant_ins150.jpg"
                },
                 new Product
                 {
                     Id = 7,
                     Title = "Apache RTR 160 4V",
                     Description = "160cc race-tuned fuel-injected engine with sporty graphics.",
                     Brand = "TVS",
                     Price = 248000,
                     ModelYear = 2022,
                     EngineCapacity = 160,
                     Mileage = 50,
                     CategoryId = 5,
                     ImageUrl = "/images/apache_rtr160.jpg"
                 },
                 new Product
                 {
                     Id = 8,
                     Title = "KTM Duke 200",
                     Description = "The KTM Duke 200 is a high-performance street motorcycle with a 199.5cc engine, offering impressive power and stylish design for enthusiasts.",
                     Brand = "KTM",
                     Price = 558000,
                     ModelYear = 2024,
                     EngineCapacity = 199,
                     Mileage = 35,
                     CategoryId = 1,
                     ImageUrl = "/images/ktm_duke_200.jpg"
                 },
                  new Product
                  {
                      Id = 9,
                      Title = "Pulsar 150",
                      Description = "Classic 149cc DTS-i engine offering power and efficiency.",
                      Brand = "Bajaj",
                      Price = 243000,
                      ModelYear = 2022,
                      EngineCapacity = 149,
                      Mileage = 55,
                      CategoryId = 5,
                      ImageUrl = "/images/pulsar_150.jpg"
                  }

            );
            modelBuilder.Entity<Company>().HasData(
             new Company
             {
                 Id = 1,
                 Name = "Tech Solution",
                 StreetAddress = "123 Tech St",
                 City = "Shanghai",
                 PostalCode = "12121",
                 State = "IL",
                 PhoneNumber = "6669990000"
             },
             new Company
             {
                 Id = 2,
                 Name = "Vivid Books",
                 StreetAddress = "999 Vid St",
                 City = "Shenzen",
                 PostalCode = "66666",
                 State = "IL",
                 PhoneNumber = "7779990000"
             },
             new Company
             {
                 Id = 3,
                 Name = "Readers Club",
                 StreetAddress = "999 Main St",
                 City = "GHuangzhou",
                 PostalCode = "99999",
                 State = "NY",
                 PhoneNumber = "1113335555"
             }
             );

        }
    }
}
