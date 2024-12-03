using Bookify.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext): base(dbContext)
        {
            
        }
        public DbSet<Category> categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, DisplayOrder = 3, Name = "Servil Liyoken" },
                new Category { Id=2, DisplayOrder=2, Name="Aenold"}
                );
        }

    }
}
