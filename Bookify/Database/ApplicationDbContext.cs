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

    }
}
