using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Smart.NotificationCenter.Data.Migrations
{
	public partial class InitializeQuartz : DbMigration
	{
		public override void Up()
		{
			Assembly currentAssembly = GetType().Assembly;

			var resourceNames = currentAssembly.GetManifestResourceNames().Where(IsSqlFile);
			foreach (var resourceName in resourceNames)
			{
				var resourceStream = currentAssembly.GetManifestResourceStream(resourceName);
				using (StreamReader reader = new StreamReader(resourceStream, Encoding.UTF8, false, 4096, false))
				{
					string sqlText = reader.ReadToEnd();

					Sql(sqlText);
				}
			}
		}

		public override void Down()
		{
		}

		private static bool IsSqlFile(string resourceName) => resourceName.EndsWith(".sql", StringComparison.OrdinalIgnoreCase);
	}
}
