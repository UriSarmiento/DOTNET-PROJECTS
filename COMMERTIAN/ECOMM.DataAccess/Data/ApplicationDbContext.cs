using ECOMM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECOMM.DataAccess.Data
{
    // Basic configuration for entity framework
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> // DbContext is the root clase of entity framework core 
    {
        // ctor + 2 tabs to create a constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)// The connection string is received as a parameter in the constructor of this class and then passed to the base class (DbContext)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } // This is to modify the default Identity User and add columns, using as base the model we created

        // Its a base class from application DB context, we are overriding it to modify the basic functioning
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder); // needed to use IdentityDbContext
            // We create a new modelBuilder object and use it to add categories to the Category table
            modelBuilder.Entity<Category>().HasData( // This function expects an array, so we add all the categories we want separated by a comma
                new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "Comedy", DisplayOrder = 3 },
                new Category { CategoryId = 4, Name = "Romance", DisplayOrder = 4 }
                );

            modelBuilder.Entity<Product>().HasData( // This function expects an array, so we add all the categories we want separated by a comma
                new Product { ProductId = 1, Title = "Coleccion Paulo Coelho", Description = "Coleccion del maestrazo", ISBN = "1024010601", Author = "Caulo Poelho", ListPrice = 20.50, Price = 5.0, Price50 = 3.5, Price100 = 2.5, CategoryId = 3, ImageUrl = "" },
                new Product { ProductId = 2, Title = "1984", Description = "Historia en un mundo distopico", ISBN = "1024010602", Author = "George Orwell", ListPrice = 20.50, Price = 5.0, Price50 = 3.5, Price100 = 2.5, CategoryId = 2, ImageUrl = "" });
        }
    }
}
