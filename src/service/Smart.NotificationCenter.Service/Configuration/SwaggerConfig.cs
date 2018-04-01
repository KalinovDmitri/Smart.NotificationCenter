using System;
using System.Web.Http;

using Swashbuckle.Application;
using WebActivatorEx;

using Smart.NotificationCenter.Service;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Smart.NotificationCenter.Service
{
	public class SwaggerConfig
	{
		public static void Register()
		{
		}
	}
}
