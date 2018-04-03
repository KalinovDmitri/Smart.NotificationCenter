using System;

namespace Smart.NotificationCenter.Data.Entities
{
	public class Notification : UpdatableEntity<Guid>
	{
		public string Title { get; set; }

		public string Body { get; set; }

		public NotificationSendingType SendingType { get; set; }

		public NotificationType Type { get; set; }

		public string JobKey { get; set; }

		public bool IsEnabled { get; set; }

		public Guid RoleId { get; set; }

		public virtual Role UserRole { get; set; }
	}
}