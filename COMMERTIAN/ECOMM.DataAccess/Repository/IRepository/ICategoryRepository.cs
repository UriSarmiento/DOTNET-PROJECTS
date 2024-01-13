using ECOMM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOMM.DataAccess.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category> // This calls the IRepository which has the generic implementation, and give it te Category model 
	{
		void Update(Category obj);

	}
}
