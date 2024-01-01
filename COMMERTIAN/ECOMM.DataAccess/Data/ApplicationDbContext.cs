using ECOMM.Models;
using Microsoft.EntityFrameworkCore;

namespace ECOMM.DataAccess.Data
{
    // Basic configuration for entity framework
    public class ApplicationDbContext : DbContext // DbContext is the root clase of entity framework core 
    {
        // ctor + 2 tabs to create a constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)// The connection string is received as a parameter in the constructor of this class and then passed to the base class (DbContext)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
		public DbSet<Users> Users { get; set; }

		// Its a base class from application DB context, we are overriding it to modify the basic functioning
		protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            // We create a new modelBuilder object and use it to add categories to the Category table
            modelBuilder.Entity<Category>().HasData( // This function expects an array, so we add all the categories we want separated by a comma
                new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "Comedy", DisplayOrder = 3 },
                new Category { CategoryId = 4, Name = "Romance", DisplayOrder = 4 }
                );
			modelBuilder.Entity<Users>().HasData( // This function expects an array, so we add all the categories we want separated by a comma
				new Users { UserId = 1, UserName = "Administrator", Password = "Admin1997", ActiveFlag = true }
				);
		}
    }
}
