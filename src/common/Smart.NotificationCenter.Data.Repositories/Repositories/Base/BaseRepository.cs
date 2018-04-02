using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Smart.NotificationCenter.Data.Abstractions;
using Smart.NotificationCenter.Data.Entities;
using Smart.NotificationCenter.Data.EntityFramework;

namespace Smart.NotificationCenter.Data.Repositories
{
	public abstract class BaseRepository<TKey, TEntity> : IRepository<TEntity> where TEntity : BaseEntity<TKey> where TKey : struct
	{
		private readonly IDbContextAccessor _contextAccessor;

		protected DbContext Context => _contextAccessor.GetContext();

		protected DbSet<TEntity> EntitySet => Context.Set<TEntity>();

		protected internal BaseRepository(IDbContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public TEntity Add(TEntity entity)
		{
			return EntitySet.Add(entity);
		}

		public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
		{
			return EntitySet.AddRange(entities);
		}

		public TEntity Remove(TEntity entity)
		{
			return EntitySet.Remove(entity);
		}

		public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
		{
			return EntitySet.RemoveRange(entities);
		}

		public Task<bool> AnyAsync(ISpecification<TEntity> specification)
		{
			return EntitySet.AnyAsync(specification.ToExpression());
		}

		public Task<int> CountAsync(ISpecification<TEntity> specification)
		{
			return EntitySet.CountAsync(specification.ToExpression());
		}

		public Task<long> LongCountAsync(ISpecification<TEntity> specification)
		{
			return EntitySet.LongCountAsync(specification.ToExpression());
		}

		public Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification)
		{
			return EntitySet.FirstOrDefaultAsync(specification.ToExpression());
		}

		public Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> specification)
		{
			return EntitySet.SingleOrDefaultAsync(specification.ToExpression());
		}

		public IQueryable<TEntity> Where(ISpecification<TEntity> specification)
		{
			return EntitySet.Where(specification.ToExpression());
		}

		public IQueryable<TEntity> Query() => EntitySet;
	}
}