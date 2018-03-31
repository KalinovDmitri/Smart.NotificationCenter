using System;
using System.Data.Common;
using System.Data.Entity;

namespace Smart.NotificationCenter.Data.EntityFramework
{
	public class SmartDbContext : DbContext
	{
		public SmartDbContext() : base() { }

		public SmartDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

		public SmartDbContext(DbConnection connection, bool contextOwnsConnection) : base(connection, contextOwnsConnection) { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			SmartDbSchema.Map(modelBuilder);
		}
	}
}