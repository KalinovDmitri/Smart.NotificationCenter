using System;
using System.Data.Common;
using System.Data.Entity;

namespace Smart.NotificationCenter.Data.EntityFramework
{
	public class SmartDbContext : DbContext
	{
		public SmartDbContext() : base("name=SmartDbContext") { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			SmartDbSchema.Map(modelBuilder);
		}
	}
}