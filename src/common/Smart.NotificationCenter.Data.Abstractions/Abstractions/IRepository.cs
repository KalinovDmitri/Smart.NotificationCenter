using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.NotificationCenter.Data.Abstractions
{
	public interface IRepository<T>
	{
		T Add(T entity);

		IEnumerable<T> AddRange(IEnumerable<T> entities);

		T Remove(T entity);

		IEnumerable<T> RemoveRange(IEnumerable<T> entities);

		Task<bool> AnyAsync(ISpecification<T> specification);

		Task<int> CountAsync(ISpecification<T> specification);

		Task<long> LongCountAsync(ISpecification<T> specification);

		Task<T> FirstOrDefaultAsync(ISpecification<T> specification);

		Task<T> SingleOrDefaultAsync(ISpecification<T> specification);

		IQueryable<T> Query();

		IQueryable<T> Where(ISpecification<T> specification);
	}
}