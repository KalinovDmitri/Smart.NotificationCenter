using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

using Smart.NotificationCenter.Data.Entities;

namespace Smart.NotificationCenter.Data.Mappings
{
	public abstract class BaseEntityMapper<TKey, TEntity> : AbstractEntityMapper<TEntity> where TEntity : BaseEntity<TKey> where TKey : struct
	{
		protected readonly bool _keyIsClustered;

		private readonly DatabaseGeneratedOption _keyGenerationOption;

		protected internal BaseEntityMapper(string tableName, bool keyIsClustered = false,
			DatabaseGeneratedOption keyGenerationOption = DatabaseGeneratedOption.Identity) : base(tableName)
		{
			_keyIsClustered = keyIsClustered;
			_keyGenerationOption = keyGenerationOption;
		}

		protected override void Map(EntityTypeConfiguration<TEntity> entityConfig)
		{
			base.Map(entityConfig);

			entityConfig.HasKey(x => x.Id, ConfigureEntityKey)
				.Property(x => x.Id)
				.HasColumnName("Id")
				.HasColumnOrder(Constants.IdColumnOrder)
				.HasDatabaseGeneratedOption(_keyGenerationOption)
				.IsRequired();
		}

		private void ConfigureEntityKey(PrimaryKeyIndexConfiguration primaryKeyConfig)
		{
			string keyName = string.Format("{0}_pk_id", _tableName).ToLowerInvariant();

			primaryKeyConfig.IsClustered(_keyIsClustered);
			primaryKeyConfig.HasName(keyName);
		}
	}
}