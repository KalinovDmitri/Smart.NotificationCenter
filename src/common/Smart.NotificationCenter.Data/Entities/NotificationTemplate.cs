using System;

namespace Smart.NotificationCenter.Data.Entities
{
	public class NotificationTemplate : UpdatableEntity<long>
	{
		public string Title { get; set; }

		public string Body { get; set; }

		public NotificationSendingType SendingType { get; set; }

		public NotificationType Type { get; set; }
	}
}