using System.Web.Http;

using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Smart.NotificationCenter.Service.UnityWebApiActivator), nameof(Smart.NotificationCenter.Service.UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Smart.NotificationCenter.Service.UnityWebApiActivator), nameof(Smart.NotificationCenter.Service.UnityWebApiActivator.Shutdown))]

namespace Smart.NotificationCenter.Service
{
	public static class UnityWebApiActivator
	{
		public static void Start()
		{
			var container = UnityConfig.GetConfiguredContainer();
			
			var resolver = new UnityDependencyResolver(container);

			GlobalConfiguration.Configuration.DependencyResolver = resolver;
		}

		/// <summary>
		/// Disposes the Unity container when the application is shut down.
		/// </summary>
		public static void Shutdown()
		{
			UnityConfig.GetConfiguredContainer().Dispose();
		}
	}
}