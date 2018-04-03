using System;
using System.Web.Http;

using Unity;

namespace Smart.NotificationCenter.Service
{
	internal class IoCConfig
	{
		public static IUnityContainer Instance { get; private set; }

		public static IUnityContainer Configure(HttpConfiguration configuration)
		{
			var container = new UnityContainer();

			container.RegisterInstance(configuration);

			// TODO: register all types here!

			Instance = container;

			return container;
		}
	}
}