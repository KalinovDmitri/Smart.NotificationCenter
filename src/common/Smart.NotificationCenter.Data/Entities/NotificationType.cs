using System;

namespace Smart.NotificationCenter.Data.Entities
{
	public enum NotificationType : int
	{
		Unknown = 0,
		NewAccountCreation,
		UpcomingPayment,
		LatePayment,
		AccountSuspension,
		UserAddedToWaitingList,
		SpaceInWaitingListBecomesAvailable,
		Custom
	}
}