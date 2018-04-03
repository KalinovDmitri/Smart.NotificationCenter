using System;

namespace Smart.NotificationCenter.Service.Dtos
{
	public enum NotificationSendingType : int
	{
		SendAsEmail,
		SendAsNotification,
		SendAsEmailAndNotification
	}
}