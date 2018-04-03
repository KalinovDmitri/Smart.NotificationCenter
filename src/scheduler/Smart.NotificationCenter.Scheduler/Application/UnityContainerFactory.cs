using System;

using NLog;

using Unity;
using Unity.Container;
using Unity.Lifetime;

namespace Smart.NotificationCenter.Scheduler
{
	internal static class UnityContainerFactory
	{
		public static IUnityContainer BuildContainer()
		{
			var container = new UnityContainer();

			container.RegisterInstance<ILogger>(LogManager.GetLogger("Service"));

			container.RegisterType<QuartzServiceHost>();

			return container;
		}
	}
}