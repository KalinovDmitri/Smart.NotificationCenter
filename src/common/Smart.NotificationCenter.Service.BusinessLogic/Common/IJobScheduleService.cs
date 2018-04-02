using System;

namespace Smart.NotificationCenter.Service.BusinessLogic
{
	public interface IJobScheduleService
	{
		void ScheduleJob(JobInfo jobInfo);
	}
}