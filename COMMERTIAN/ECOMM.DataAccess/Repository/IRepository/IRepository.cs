using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECOMM.DataAccess.Repository.IRepository
{
	public interface IRepository <T> where T : class
	{
		// The update is not in the generic repository as logic for updates might be differente depending on the model
		//T es category o cualquier otro modelo que usemos
		IEnumerable<T> GetAll (); //esto es por si se necesita regresar todo
		T Get(Expression<Func<T, bool>> filter); //Sintaxis general para Linq expressions, para regresar 1 solo valor en esta caso
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entity);
	}
}
