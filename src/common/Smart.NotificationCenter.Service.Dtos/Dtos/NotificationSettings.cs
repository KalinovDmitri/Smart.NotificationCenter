using System;

namespace Smart.NotificationCenter.Service.Dtos
{
	public class NotificationSettings
	{
		public NotificationSendingType SendingType { get; set; }

		public NotificationRepeatType RepeatType { get; set; }

		public int? RepeatsEvery { get; set; }

		public NotificationEndingType EndingType { get; set; }

		public DateTime? EndingDate { get; set; }

		public DateTime? SendingDate { get; set; }
	}
}