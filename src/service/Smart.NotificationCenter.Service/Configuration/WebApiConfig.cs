using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Smart.NotificationCenter.Service
{
	public static class WebApiConfig
	{
		public static void Configure(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
