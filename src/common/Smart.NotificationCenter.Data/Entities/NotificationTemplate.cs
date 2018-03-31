using System;

namespace Smart.NotificationCenter.Data.Entities
{
	public class NotificationTemplate
	{
		public long Id { get; set; }

		public string Title { get; set; }

		public string Body { get; set; }

		public NotificationSendingType SendingType { get; set; }

		public NotificationType Type { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}
}