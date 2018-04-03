using System;

using Quartz;
using Quartz.Collection;
using Quartz.Core;
using Quartz.Impl;

using Smart.NotificationCenter.Service.Dtos;

namespace Smart.NotificationCenter.Service.BusinessLogic
{
	public class DefaultJobFactory : IJobFactory
	{
		public JobInfo CreateJob<TJob>(NotificationDto notificationInfo, Guid notificationId, string group)
		{
			IJobDetail jobDetail = JobBuilder.Create()
				.OfType<TJob>()
				.RequestRecovery(true)
				.WithIdentity(notificationId.ToString("B"), group)
				.WithDescription(notificationInfo.Title)
				.UsingJobData("RoleId", notificationInfo.RoleId.ToString("B"))
				.UsingJobData("NotificationId", notificationId.ToString("B"))
				.Build();

			var triggerBuilder = TriggerBuilder.Create()
				.WithIdentity(notificationInfo.Title, group);

			ApplySettings(triggerBuilder, notificationInfo.Settings);

			triggerBuilder.ForJob(jobDetail.Key);

			ITrigger trigger = triggerBuilder.Build();

			return new JobInfo(jobDetail, trigger);
		}

		private void ApplySettings(TriggerBuilder triggerBuilder, NotificationSettings settings)
		{
			if (settings.SendingDate.HasValue)
			{
				var offset = new DateTimeOffset(settings.SendingDate.Value.ToUniversalTime());

				triggerBuilder.StartAt(offset);
			}

			if (settings.EndingType == NotificationEndingType.OnDate)
			{
				var offset = new DateTimeOffset(settings.EndingDate.Value.ToUniversalTime());

				triggerBuilder.EndAt(offset);
			}

			triggerBuilder.WithDailyTimeIntervalSchedule((builder) =>
			{
				if (settings.RepeatType != NotificationRepeatType.NoRepeat)
				{
					var repeatCount = settings.RepeatsCount.GetValueOrDefault(0);
					var repeatInterval = settings.RepeatsEvery.GetValueOrDefault(1);

					switch (settings.RepeatType)
					{
						case NotificationRepeatType.Daily:
							builder.OnEveryDay().WithInterval(repeatInterval, IntervalUnit.Hour);
							break;
					}

					if (repeatCount != 0)
						builder.WithRepeatCount(repeatCount);
				}
			});
		}
	}
}