using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Smart.NotificationCenter.Data.Entities;

namespace Smart.NotificationCenter.Data.Mappings
{
	public sealed class NotificationMapper : UpdatableEntityMapper<Guid, Notification>
	{
		public NotificationMapper() : base("Notifications") { }

		protected override void Map(EntityTypeConfiguration<Notification> entityConfig)
		{
			base.Map(entityConfig);

			int columnOrder = Constants.ColumnsStartOrder;

			entityConfig.Property(x => x.RoleId)
				.HasColumnOrder(columnOrder++)
				.IsRequired();

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

			entityConfig.Property(x => x.IsEnabled)
				.HasColumnOrder(columnOrder++)
				.IsRequired();

			entityConfig.Property(x => x.SendingType)
				.HasColumnOrder(columnOrder++)
				.HasColumnType("int")
				.IsRequired();

			entityConfig.Property(x => x.Type)
				.HasColumnOrder(columnOrder++)
				.HasColumnType("int")
				.IsRequired();

			entityConfig.Property(x => x.JobKey)
				.HasColumnOrder(columnOrder++)
				.HasMaxLength(128)
				.IsRequired();

			entityConfig.HasRequired(x => x.UserRole)
				.WithMany(x => x.Notifications)
				.HasForeignKey(x => x.RoleId)
				.WillCascadeOnDelete(false);
		}
	}
}