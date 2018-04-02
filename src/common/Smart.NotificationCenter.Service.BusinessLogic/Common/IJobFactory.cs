using System;

using Smart.NotificationCenter.Service.Dtos;

namespace Smart.NotificationCenter.Service.BusinessLogic
{
	public interface IJobFactory
	{
		JobInfo CreateJob<TJob>(NotificationDto notification, string group);
	}
}