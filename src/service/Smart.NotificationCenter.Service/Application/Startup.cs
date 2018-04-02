using System;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

using Microsoft.Owin;
using Owin;

using NLog;
using Unity;
using Unity.AspNet.WebApi;

using Smart.NotificationCenter.Service;

[assembly: OwinStartup(typeof(Startup), nameof(Startup.BuildApplication))]

namespace Smart.NotificationCenter.Service
{
	public class Startup
	{
		public void BuildApplication(IAppBuilder appBuilder)
		{
			var configuration = GlobalConfiguration.Configuration;

			var container = IoCConfig.Configure(configuration);

			configuration.DependencyResolver = new UnityDependencyResolver(container);

			DatabaseConfig.Configure();
			CorsConfig.Configure(configuration);
			WebApiConfig.Configure(configuration);
			SwaggerConfig.Configure(configuration);

			configuration.EnsureInitialized();
		}
	}
}