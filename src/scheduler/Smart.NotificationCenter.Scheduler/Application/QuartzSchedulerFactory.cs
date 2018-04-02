using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using Unity;
using Quartz;
using Quartz.Core;
using Quartz.Impl;
using Quartz.Spi;

namespace Smart.NotificationCenter.Scheduler
{
	internal class QuartzSchedulerFactory
	{
		private const string ConnectionStrng = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Smart_NotificationCenter;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		private readonly UnityJobFactory _jobFactory;

		public QuartzSchedulerFactory(UnityJobFactory jobFactory)
		{
			_jobFactory = jobFactory;
		}

		public IScheduler CreateScheduler()
		{
			var configuration = new NameValueCollection
			{
				["quartz.scheduler.instanceName"] = "SmartJobScheduler",
				["quartz.threadPool.threadCount"] = "10",
				["quartz.threadPool.threadPriority"] = "Normal",
				["quartz.scheduler.idleWaitTime"] = "30000",
				["quartz.jobStore.misfireThreshold"] = "60000",
				["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
				["quartz.jobStore.tablePrefix"] = "QRTZ_",
				["quartz.jobStore.clustered"] = "false",
				["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
				["quartz.jobStore.dataSource"] = "SqlServerFullVersion",
				["quartz.dataSource.SqlServerFullVersion.connectionString"] = ConnectionStrng,
				["quartz.dataSource.SqlServerFullVersion.provider"] = "SqlServer-20",
				["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz",
				["quartz.scheduler.exporter.port"] = "5001",
				["quartz.scheduler.exporter.bindName"] = "QuartzScheduler",
				["quartz.scheduler.exporter.channelType"] = "tcp",
				["quartz.scheduler.exporter.channelName"] = "httpQuartz",
				["quartz.scheduler.exporter.rejectRemoteRequests"] = "false"
			};

			var schedulerFactory = new StdSchedulerFactory(configuration);

			var scheduler = schedulerFactory.GetScheduler();

			scheduler.JobFactory = _jobFactory;
			scheduler.Start();

			return scheduler;
		}
	}
}