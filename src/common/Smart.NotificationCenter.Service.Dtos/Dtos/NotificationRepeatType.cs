using System;

namespace Smart.NotificationCenter.Service.Dtos
{
	public enum NotificationRepeatType : int
	{
		NoRepeat,
		Daily,
		Weekly,
		Monthly,
		Annualy,
		Custom
	}
}