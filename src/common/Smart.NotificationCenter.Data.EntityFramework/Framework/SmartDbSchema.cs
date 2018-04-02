using System;
using System.Collections.Generic;
using System.Data.Entity;

using Smart.NotificationCenter.Data.Mappings;

namespace Smart.NotificationCenter.Data.EntityFramework
{
	public static class SmartDbSchema
	{
		public static void Map(DbModelBuilder modelBuilder)
		{
			var mappers = GetEntityMappers();

			foreach (var mapper in mappers)
			{
				mapper.Map(modelBuilder);
			}
		}

		public static IEnumerable<IEntityMapper> GetEntityMappers()
		{
			yield return new UserMapper();
			yield return new RoleMapper();
			yield return new NotificationMapper();
			yield return new NotificationTemplateMapper();
		}
	}
}