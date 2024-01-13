using ECOMM.DataAccess.Data;
using ECOMM.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ECOMM.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext _db;

		public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
        }
	 

       /* public UnitOfWork(ApplicationDbContext db, string ModelType)
        {
			_db = db;

			switch(ModelType)
			{
				case ("PRODUCT"):
                    Category = new CategoryRepository(_db);
                    break;

                case ("CATEGORY"):
                    Category = new CategoryRepository(_db);
                    break;

                case ("USERS"):

                break;
            }

        }*/

        public void Save()
		{
			_db.SaveChanges();
		}
	}
}
