using System;

using Quartz;
using CrystalQuartz.Core.SchedulerProviders;

namespace Smart.NotificationCenter.Scheduler
{
	internal class SimpleSchedulerProvider : ISchedulerProvider
	{
		private readonly IScheduler _scheduler;

		public IScheduler Scheduler => _scheduler;

		public SimpleSchedulerProvider(IScheduler scheduler)
		{
			_scheduler = scheduler;
		}

		public void Init() { }
	}
}