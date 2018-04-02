using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Smart.NotificationCenter.Data.Entities;

namespace Smart.NotificationCenter.Data.Mappings
{
	public sealed class RoleMapper : UpdatableEntityMapper<Guid, Role>
	{
		public RoleMapper() : base("Roles") { }

		protected override void Map(EntityTypeConfiguration<Role> entityConfig)
		{
			base.Map(entityConfig);

			int columnOrder = Constants.ColumnsStartOrder;

			entityConfig.Property(x => x.Name)
				.HasColumnOrder(columnOrder++)
				.HasMaxLength(64)
				.IsRequired();

			entityConfig.Property(x => x.Available)
				.HasColumnOrder(columnOrder++)
				.IsRequired();

			entityConfig.HasMany(x => x.Users)
				.WithMany(x => x.Roles);

			entityConfig.HasMany(x => x.Notifications)
				.WithRequired(x => x.UserRole)
				.HasForeignKey(x => x.RoleId)
				.WillCascadeOnDelete(false);
		}
	}
}