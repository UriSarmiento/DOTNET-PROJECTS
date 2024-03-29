using ECOMM.DataAccess.Data;
using ECOMM.DataAccess.Repository.IRepository;
using ECOMM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOMM.DataAccess.Repository
{ 
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db) // Repository category received an ApplicationDbContext so we use base to pass it to the mother class
        {
            _db = db;
        }


        public void Update(Product obj)
        {
            var objFromDB = _db.Product.FirstOrDefault(u=>u.ProductId == obj.ProductId);
            if (objFromDB is not null)
            {
				objFromDB.Title = obj.Title;
				objFromDB.ISBN = obj.ISBN;
				objFromDB.Price = obj.Price;
				objFromDB.Price50 = obj.Price50;
				objFromDB.ListPrice = obj.ListPrice;
				objFromDB.Price100 = obj.Price100;
				objFromDB.Description = obj.Description;
				objFromDB.CategoryId = obj.CategoryId;
				objFromDB.Author = obj.Author;
                if (obj.ImageUrl is not null)
                {
                    objFromDB.ImageUrl = obj.ImageUrl;

				}


			}
        }


    }
}
