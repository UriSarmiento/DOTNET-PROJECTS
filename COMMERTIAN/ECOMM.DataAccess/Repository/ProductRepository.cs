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
            _db.Product.Update(obj);
        }


    }
}
