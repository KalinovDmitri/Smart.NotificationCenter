using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Smart.NotificationCenter.Service
{
	internal static class CorsConfig
	{
		public static void Configure(HttpConfiguration configuration)
		{
			var corsOptions = new EnableCorsAttribute("*", "*", "GET,POST,PUT,DELETE,OPTIONS");

			configuration.EnableCors(corsOptions);
		}
	}
}