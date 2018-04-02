using System;

namespace Smart.NotificationCenter.Data.Entities
{
	[Flags]
	public enum NotificationSendingType : int
	{
		None = 0,
		Email,
		Notification
	}
}