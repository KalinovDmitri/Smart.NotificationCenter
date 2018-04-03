using System;
using System.Collections.Generic;

namespace Smart.NotificationCenter.Service.Dtos
{
	public class NotificationSettings
	{
		public NotificationSendingType SendingType { get; set; }

		public NotificationRepeatType RepeatType { get; set; }

		public int? RepeatsCount { get; set; }

		public int? RepeatsEvery { get; set; }

		public List<DayOfWeek> RepeatDays { get; set; }

		public NotificationEndingType EndingType { get; set; }

		public DateTime? EndingDate { get; set; }

		public DateTime? SendingDate { get; set; }
	}
}