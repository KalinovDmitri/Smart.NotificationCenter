using System;
using System.Data.Entity;

using Smart.NotificationCenter.Data.EntityFramework;
using Smart.NotificationCenter.Data.Migrations;

namespace Smart.NotificationCenter.Service
{
	internal static class DatabaseConfig
	{
		public static void Configure()
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<SmartDbContext, SmartMigrationsConfiguration>(true));
		}
	}
}