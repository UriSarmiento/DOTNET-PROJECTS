﻿using ECOMM.Models;
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
        public DbSet<Product> Product { get; set; }

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

            modelBuilder.Entity<Product>().HasData( // This function expects an array, so we add all the categories we want separated by a comma
                new Product { ProductId = 1, Title = "Coleccion Paulo Coelho", Description = "Coleccion del maestrazo", ISBN = "1024010601", Author = "Caulo Poelho", ListPrice = 20.50, Price = 5.0, Price50 = 3.5, Price100 = 2.5, CategoryId = 3, ImageUrl = "" },
                new Product { ProductId = 2, Title = "El libro troll del Rubius", Description = "Obra maestra de la literatura moderna", ISBN = "1024010602", Author = "Ay Rubiuh no seas malo", ListPrice = 20.50, Price = 5.0, Price50 = 3.5, Price100 = 2.5, CategoryId = 2, ImageUrl = "" },
                new Product { ProductId = 3, Title = "El cipote de la mancha", Description = "Clasico", ISBN = "1024010603", Author = "Sancho Panza", ListPrice = 20.50, Price = 5.0, Price50 = 3.5, Price100 = 2.5 , CategoryId = 4, ImageUrl = "" },
                new Product { ProductId = 4, Title = "Mein Kampf", Description = "Senor de bigote chistoso escribe un libro y lo publica", ISBN = "1024010604", Author = "Charlie Chaplin", ListPrice = 20.50, Price = 5.0, Price50 = 3.5, Price100 = 2.5, CategoryId = 1, ImageUrl = "" }
                );
        }
    }
}
