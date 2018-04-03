using System;

using NLog;

using Unity;
using Unity.Container;
using Unity.Lifetime;

using Smart.NotificationCenter.Jobs;
using Smart.NotificationCenter.Jobs.Implementation;

namespace Smart.NotificationCenter.Scheduler
{
	internal static class UnityContainerFactory
	{
		public static IUnityContainer BuildContainer()
		{
			var container = new UnityContainer();

			container.RegisterInstance<ILogger>(LogManager.GetLogger("Service"));

			container.RegisterSingleton<UnityJobFactory>();
			container.RegisterSingleton<QuartzServiceHost>();

			container.RegisterType<CustomNotificationJob, CustomNotificationJobImpl>();

			return container;
		}
	}
}