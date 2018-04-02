using System;

using Quartz;

namespace Smart.NotificationCenter.Jobs.Implementation
{
	public class CustomNotificationJob : IJob, ICustomNotification
	{
		public void Execute(IJobExecutionContext context)
		{

		}
	}
}