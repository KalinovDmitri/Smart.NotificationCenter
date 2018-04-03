using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Smart.NotificationCenter.Data.Entities;

namespace Smart.NotificationCenter.Data.Mappings
{
	public abstract class UpdatableEntityMapper<TKey, TEntity> : CreatableEntityMapper<TKey, TEntity> where TEntity : UpdatableEntity<TKey> where TKey : struct
	{
		protected internal UpdatableEntityMapper(string tableName,
			bool keyIsClustered = false,
			DatabaseGeneratedOption keyGenerationOption = DatabaseGeneratedOption.Identity) :
			base(tableName, keyIsClustered, keyGenerationOption) { }

		protected override void Map(EntityTypeConfiguration<TEntity> entityConfig)
		{
			base.Map(entityConfig);

			entityConfig.Property(x => x.UpdatedAt)
				.HasColumnOrder(Constants.UpdatedAtColumnOrder)
				.HasColumnType("datetime2")
				.IsOptional();
		}
	}
}