using ECOMM.DataAccess.Data;
using ECOMM.DataAccess.Repository.IRepository;
using ECOMM.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECOMM.DataAccess.Repository
{
	public class CategoryRepository :  Repository<Category>, ICategoryRepository  // CTRL + . to implement in visual studio
	{
		private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db) // Repository category received an ApplicationDbContext so we use base to pass it to the mother class
        {
			_db = db;
        }




		public void Update(Category obj)
		{
			_db.Categories.Update(obj);
		}
	}
}
