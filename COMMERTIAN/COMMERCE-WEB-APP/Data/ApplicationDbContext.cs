using COMMERCE_WEB_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace COMMERCE_WEB_APP.Data
{
    // Basic configuration for entity framework
    public class ApplicationDbContext : DbContext // DbContext is the root clase of entity framework core 
    {
        // ctor + 2 tabs to create a constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)// The connection string is received as a parameter in the constructor of this class and then passed to the base class (DbContext)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
    }
}
