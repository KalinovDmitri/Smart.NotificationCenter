using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

using Smart.NotificationCenter.Data.Entities;

namespace Smart.NotificationCenter.Data.Mappings
{
	public interface IEntityMapper
	{
		void Map(DbModelBuilder modelBuilder);
	}

	public abstract class AbstractEntityMapper<TEntity> : IEntityMapper where TEntity : AbstractEntity
	{
		private const string SchemaName = "dbo";

		protected readonly string _tableName;

		protected internal AbstractEntityMapper(string tableName)
		{
			if (string.IsNullOrEmpty(tableName))
			{
				throw new ArgumentNullException(nameof(tableName), "Table name can't be null or empty.");
			}

			_tableName = tableName;
		}

		public void Map(DbModelBuilder modelBuilder)
		{
			var entityConfig = modelBuilder.Entity<TEntity>();

			Map(entityConfig);
		}

		protected virtual void Map(EntityTypeConfiguration<TEntity> entityConfig)
		{
			entityConfig.ToTable(_tableName, SchemaName);
		}
	}
}