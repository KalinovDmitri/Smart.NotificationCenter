using System;

using Quartz;

namespace Smart.NotificationCenter.Service.BusinessLogic
{
	public class JobInfo
	{
		public IJobDetail Job { get; private set; }

		public ITrigger Trigger { get; private set; }

		public JobInfo(IJobDetail job, ITrigger trigger)
		{
			Job = job;
			Trigger = trigger;
		}
	}
}