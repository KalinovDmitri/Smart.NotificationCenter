using System;
using System.Data.Entity.ModelConfiguration;

using Smart.NotificationCenter.Data.Entities;

namespace Smart.NotificationCenter.Data.Mappings
{
	public sealed class NotificationTemplateMapper : UpdatableEntityMapper<long, NotificationTemplate>
	{
		public NotificationTemplateMapper() : base("NotificationTemplates") { }

		protected override void Map(EntityTypeConfiguration<NotificationTemplate> entityConfig)
		{
			base.Map(entityConfig);

			int columnOrder = Constants.ColumnsStartOrder;

			entityConfig.Property(x => x.Title)
				.HasColumnOrder(columnOrder++)
				.HasMaxLength(256)
				.IsUnicode(true)
				.IsRequired();

			entityConfig.Property(x => x.Body)
				.HasColumnOrder(columnOrder++)
				.IsMaxLength()
				.IsUnicode(true)
				.IsRequired();

			entityConfig.Property(x => x.SendingType)
				.HasColumnOrder(columnOrder++)
				.HasColumnType("int")
				.IsRequired();

			entityConfig.Property(x => x.Type)
				.HasColumnOrder(columnOrder++)
				.HasColumnType("int")
				.IsRequired();
		}
	}
}