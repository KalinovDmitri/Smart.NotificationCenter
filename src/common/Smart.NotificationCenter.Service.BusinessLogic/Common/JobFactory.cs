using System;

using Quartz;
using Quartz.Core;
using Quartz.Impl;

using Smart.NotificationCenter.Service.Dtos;

namespace Smart.NotificationCenter.Service.BusinessLogic
{
	public class DefaultJobFactory : IJobFactory
	{
		public JobInfo CreateJob<TJob>(NotificationDto notification, string group)
		{
			IJobDetail jobDetail = JobBuilder.Create()
				.OfType<TJob>()
				.WithIdentity(notification.Title, group)
				.WithDescription(notification.Title)
				.Build();

			var triggerBuilder = TriggerBuilder.Create()
				.WithIdentity(notification.Title, group);

			ApplySettings(triggerBuilder, notification.Settings);

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
					switch (settings.RepeatType)
					{
						case NotificationRepeatType.Daily:
							builder.OnEveryDay();
							break;
					}
				}

				builder.WithRepeatCount(settings.RepeatsEvery ?? 1);
			});
		}
	}
}