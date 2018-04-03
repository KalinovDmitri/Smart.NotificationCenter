namespace Smart.NotificationCenter.Data.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class InitialMigration : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Users",
				c => new
				{
					Id = c.Guid(nullable: false, identity: true),
					AccountName = c.String(nullable: false, maxLength: 128),
					Email = c.String(nullable: false, maxLength: 128),
					PasswordHash = c.String(nullable: false),
					FirstName = c.String(nullable: false, maxLength: 128),
					LastName = c.String(nullable: false, maxLength: 128),
					LockedOut = c.Boolean(),
					CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2", defaultValueSql: "GETUTCDATE()"),
					UpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
				})
				.PrimaryKey(t => t.Id, name: "users_pk_id", clustered: false);

			CreateTable(
				"dbo.Roles",
				c => new
				{
					Id = c.Guid(nullable: false, identity: true),
					Name = c.String(nullable: false, maxLength: 64),
					Available = c.Boolean(nullable: false),
					CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2", defaultValueSql: "GETUTCDATE()"),
					UpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
				})
				.PrimaryKey(t => t.Id, name: "roles_pk_id", clustered: false);

			CreateTable(
				"dbo.Notifications",
				c => new
				{
					Id = c.Guid(nullable: false, identity: true),
					RoleId = c.Guid(nullable: false),
					Title = c.String(nullable: false, maxLength: 256),
					Body = c.String(nullable: false),
					IsEnabled = c.Boolean(nullable: false),
					SendingType = c.Int(nullable: false),
					Type = c.Int(nullable: false),
					JobKey = c.String(nullable: false, maxLength: 128),
					CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2", defaultValueSql: "GETUTCDATE()"),
					UpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
				})
				.PrimaryKey(t => t.Id, name: "notifications_pk_id", clustered: false)
				.ForeignKey("dbo.Roles", t => t.RoleId)
				.Index(t => t.RoleId);

			CreateTable(
				"dbo.NotificationTemplates",
				c => new
				{
					Id = c.Long(nullable: false, identity: true),
					Title = c.String(nullable: false, maxLength: 256),
					Body = c.String(nullable: false),
					SendingType = c.Int(nullable: false),
					Type = c.Int(nullable: false),
					CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2", defaultValueSql: "GETUTCDATE()"),
					UpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
				})
				.PrimaryKey(t => t.Id, name: "notificationtemplates_pk_id", clustered: false);

			CreateTable(
				"dbo.UserRoles",
				c => new
				{
					UserId = c.Guid(nullable: false),
					RoleId = c.Guid(nullable: false),
				})
				.PrimaryKey(t => new { t.UserId, t.RoleId })
				.ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
				.ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
				.Index(t => t.UserId)
				.Index(t => t.RoleId);

		}

		public override void Down()
		{
			DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
			DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
			DropForeignKey("dbo.Notifications", "RoleId", "dbo.Roles");
			DropIndex("dbo.UserRoles", new[] { "RoleId" });
			DropIndex("dbo.UserRoles", new[] { "UserId" });
			DropIndex("dbo.Notifications", new[] { "RoleId" });
			DropTable("dbo.UserRoles");
			DropTable("dbo.NotificationTemplates");
			DropTable("dbo.Notifications");
			DropTable("dbo.Roles");
			DropTable("dbo.Users");
		}
	}
}
