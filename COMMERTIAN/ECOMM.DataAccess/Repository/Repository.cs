using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECOMM.DataAccess.Data;
using ECOMM.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ECOMM.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet; //dbSet its a connection, in this case it has the Categories table, so we can operate with it

        public Repository(ApplicationDbContext db)
        {
			_db = db;	
			this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public T Get(Expression<Func<T, bool>> filter)
		{
			IQueryable<T> query = dbSet; // Here create an object Query and i store the categories table
			query = query.Where(filter); // Here i apply a filter to the query object so i just have the values needed
			return query.FirstOrDefault();
		}

		public IEnumerable<T> GetAll()
		{
			IQueryable<T> query = dbSet;
			return query.ToList(); // As i have the whole categories table, i convert it to a list and return it
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}
	}
}
