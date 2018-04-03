using System;
using System.Data.Entity;

using Unity;

namespace Smart.NotificationCenter.Data.EntityFramework
{
	internal class DbContextAccessor<TContext> : IDbContextAccessor where TContext : DbContext
	{
		private readonly IUnityContainer _container;

		public DbContextAccessor(IUnityContainer container)
		{
			_container = container;
		}

		public DbContext GetContext()
		{
			return _container.Resolve<TContext>();
		}
	}
}