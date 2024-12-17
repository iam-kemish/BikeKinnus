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
                    Price = 1200,
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
                    Price = 1500,
                    ModelYear = 2023,
                    EngineCapacity = 199,
                    Mileage = 35,
                    CategoryId = 2,
                    ImageUrl = "/images/pulsar_ns200.jpg"
                },
                new Product
                {
                    Id = 3,
                    Title = "Apache RTR 160 4V",
                    Description = "160cc race-tuned fuel-injected engine with sporty graphics.",
                    Brand = "TVS",
                    Price = 1100,
                    ModelYear = 2022,
                    EngineCapacity = 160,
                    Mileage = 50,
                    CategoryId = 5,
                    ImageUrl = "/images/apache_rtr160.jpg"
                },
                new Product
                {
                    Id = 4,
                    Title = "Pulsar 150",
                    Description = "Classic 149cc DTS-i engine offering power and efficiency.",
                    Brand = "Bajaj",
                    Price = 1000,
                    ModelYear = 2022,
                    EngineCapacity = 149,
                    Mileage = 55,
                    CategoryId = 5,
                    ImageUrl = "/images/pulsar_150.jpg"
                },
                new Product
                {
                    Id = 5,
                    Title = "Dominar 400",
                    Description = "373cc sports-touring beast with premium performance and LED headlamps.",
                    Brand = "Bajaj",
                    Price = 2500,
                    ModelYear = 2023,
                    EngineCapacity = 373,
                    Mileage = 30,
                    CategoryId = 4,
                    ImageUrl = "/images/dominar_400.jpg"
                },
                new Product
                {
                    Id = 6,
                    Title = "Suzuki Gixxer 160",
                    Description = "Sporty 155cc engine with aggressive styling and aerodynamic design.",
                    Brand = "Suzuki",
                    Price = 1300,
                    ModelYear = 2023,
                    EngineCapacity = 155,
                    Mileage = 45,
                    CategoryId = 2,
                    ImageUrl = "/images/suzuki_gixxer_160.jpg"
                },
                new Product
                {
                    Id = 7,
                    Title = "Avenger 160 Cruise",
                    Description = "160cc engine offering relaxed ergonomics for long-distance cruising.",
                    Brand = "Bajaj",
                    Price = 1400,
                    ModelYear = 2022,
                    EngineCapacity = 160,
                    Mileage = 45,
                    CategoryId = 3,
                    ImageUrl = "/images/avenger_160_cruise.jpg"
                },
                new Product
                {
                    Id = 8,
                    Title = "Vikrant INS 150",
                    Description = "A tribute to the INS Vikrant with a 149cc engine for efficient performance.",
                    Brand = "Bajaj",
                    Price = 950,
                    ModelYear = 2021,
                    EngineCapacity = 149,
                    Mileage = 55,
                    CategoryId = 5,
                    ImageUrl = "/images/vikrant_ins150.jpg"
                }
            );
        }
    }
}
