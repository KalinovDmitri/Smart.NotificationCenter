using System;
using System.Collections.Specialized;

using Quartz;
using Quartz.Impl;

namespace Smart.NotificationCenter.Service.BusinessLogic
{
	public class JobScheduleService : IJobScheduleService
	{
		private readonly ISchedulerFactory _schedulerFactory;

		public JobScheduleService()
		{
			var settings = new NameValueCollection
			{
				["quartz.scheduler.instanceName"] = "RemoteClient",
				["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
				["quartz.threadPool.threadCount"] = "5",
				["quartz.scheduler.proxy"] = "true",
				["quartz.scheduler.proxy.address"] = "tcp://PC-2535:5001/QuartzScheduler"
			};

			_schedulerFactory = new StdSchedulerFactory(settings);
		}

		public void ScheduleJob(JobInfo jobInfo)
		{
			var scheduler = _schedulerFactory.GetScheduler();

			scheduler.ScheduleJob(jobInfo.Job, jobInfo.Trigger);
		}
	}
}