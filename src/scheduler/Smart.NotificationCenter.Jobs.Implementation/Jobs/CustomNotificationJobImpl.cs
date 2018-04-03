using System;
using System.Collections.Generic;
using System.Linq;

using NLog;
using Quartz;

namespace Smart.NotificationCenter.Jobs.Implementation
{
	public class CustomNotificationJobImpl : CustomNotificationJob, IJob
	{
		private ILogger _logger;

		public CustomNotificationJobImpl(ILogger logger)
		{
			_logger = logger;
		}

		public override void Execute(IJobExecutionContext context)
		{
			_logger.Debug("Executing CustomNotificationJob; job key is {0}", context.JobDetail.Key);

			foreach (var item in context.JobDetail.JobDataMap)
			{
				_logger.Debug(string.Format("{0} -> {1}", item.Key, item.Value));
			}
		}
	}
}