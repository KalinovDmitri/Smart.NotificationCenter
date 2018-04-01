using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

using Smart.NotificationCenter.Data.Entities;
using Smart.NotificationCenter.Data.EntityFramework;

namespace Smart.NotificationCenter.Data.Migrations
{
	public sealed class SmartMigrationsConfiguration : DbMigrationsConfiguration<SmartDbContext>
	{
		public SmartMigrationsConfiguration()
		{
			AutomaticMigrationsEnabled = false;
			CommandTimeout = 600;
		}

		protected override void Seed(SmartDbContext context)
		{
			base.Seed(context);
		}
	}
}