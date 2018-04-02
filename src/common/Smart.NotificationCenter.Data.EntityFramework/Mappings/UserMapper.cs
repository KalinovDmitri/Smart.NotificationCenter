using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

using Smart.NotificationCenter.Data.Entities;

namespace Smart.NotificationCenter.Data.Mappings
{
	public sealed class UserMapper : UpdatableEntityMapper<Guid, User>
	{
		public UserMapper() : base("Users") { }

		protected override void Map(EntityTypeConfiguration<User> entityConfig)
		{
			base.Map(entityConfig);

			int columnOrder = Constants.ColumnsStartOrder;

			entityConfig.Property(x => x.AccountName)
				.HasColumnOrder(columnOrder++)
				.HasMaxLength(128)
				.IsRequired();

			entityConfig.Property(x => x.Email)
				.HasColumnOrder(columnOrder++)
				.HasMaxLength(128)
				.IsRequired();

			entityConfig.Property(x => x.PasswordHash)
				.HasColumnOrder(columnOrder++)
				.IsMaxLength()
				.IsRequired();

			entityConfig.Property(x => x.FirstName)
				.HasColumnOrder(columnOrder++)
				.HasMaxLength(128)
				.IsRequired();

			entityConfig.Property(x => x.LastName)
				.HasColumnOrder(columnOrder++)
				.HasMaxLength(128)
				.IsRequired();

			entityConfig.Property(x => x.LockedOut)
				.HasColumnOrder(columnOrder++)
				.IsOptional();

			entityConfig.HasMany(x => x.Roles)
				.WithMany(x => x.Users)
				.Map(MapUserRoles);
		}

		private static void MapUserRoles(ManyToManyAssociationMappingConfiguration config)
		{
			config.MapLeftKey("UserId");
			config.MapRightKey("RoleId");
			config.ToTable("UserRoles");
		}
	}
}