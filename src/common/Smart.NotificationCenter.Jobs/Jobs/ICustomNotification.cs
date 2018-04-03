using System;

using Quartz;

namespace Smart.NotificationCenter.Jobs
{
	[PersistJobDataAfterExecution]
	public abstract class CustomNotificationJob : IJob
	{
		public abstract void Execute(IJobExecutionContext context);
	}
}