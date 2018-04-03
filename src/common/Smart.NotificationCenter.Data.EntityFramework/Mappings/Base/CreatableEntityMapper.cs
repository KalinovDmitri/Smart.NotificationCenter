using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Smart.NotificationCenter.Data.Entities;

namespace Smart.NotificationCenter.Data.Mappings
{
	public abstract class CreatableEntityMapper<TKey, TEntity> : BaseEntityMapper<TKey, TEntity> where TEntity : CreatableEntity<TKey> where TKey : struct
	{
		protected internal CreatableEntityMapper(string tableName,
			bool keyIsClustered = false,
			DatabaseGeneratedOption keyGenerationOption = DatabaseGeneratedOption.Identity) :
			base(tableName, keyIsClustered, keyGenerationOption) { }

		protected override void Map(EntityTypeConfiguration<TEntity> entityConfig)
		{
			base.Map(entityConfig);

			entityConfig.Property(x => x.CreatedAt)
				.HasColumnOrder(Constants.CreatedAtColumnOrder)
				.HasColumnType("datetime2")
				.IsRequired();
		}
	}
}