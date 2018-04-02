using System;
using System.Globalization;

using Unity;
using Quartz;
using Quartz.Spi;

namespace Smart.NotificationCenter.Scheduler
{
	internal class UnityJobFactory : IJobFactory
	{
		private readonly IUnityContainer _container;

		public UnityJobFactory(IUnityContainer container)
		{
			_container = container;
		}

		public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
		{
			IJobDetail jobDetail = bundle.JobDetail;
			try
			{
				return new UnityJobWrapper(bundle, _container);
			}
			catch (Exception exc)
			{
				throw new SchedulerException(
					string.Format(CultureInfo.InvariantCulture,
					"Problem instantiating class '{0}'",
					jobDetail.JobType.FullName),
					exc);
			}
		}

		public void ReturnJob(IJob job)
		{
		}

		private class UnityJobWrapper : IJob
		{
			private readonly TriggerFiredBundle _bundle;
			private readonly IUnityContainer _container;

			protected IJob RunningJob { get; private set; }

			public UnityJobWrapper(TriggerFiredBundle bundle, IUnityContainer container)
			{
				_bundle = bundle ?? throw new ArgumentNullException(nameof(bundle));
				_container = container ?? throw new ArgumentNullException(nameof(container));
			}

			public void Execute(IJobExecutionContext context)
			{
				var childContainer = _container.CreateChildContainer();

				try
				{
					RunningJob = childContainer.Resolve(_bundle.JobDetail.JobType) as IJob;
					RunningJob.Execute(context);
				}
				catch (JobExecutionException)
				{
					throw;
				}
				catch (Exception exc)
				{
					throw new JobExecutionException(string.Format(CultureInfo.InvariantCulture, "Failed to execute Job '{0}' of type '{1}'",
						_bundle.JobDetail.Key,
						_bundle.JobDetail.JobType),
						exc);
				}
				finally
				{
					RunningJob = null;
					childContainer?.Dispose();
				}
			}
		}
	}
}