using System;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;


using Microsoft.Owin;
using Owin;

using NLog;
using Unity;

[assembly: OwinStartup(typeof(Smart.NotificationCenter.Service.Startup), "BuildApplication")]

namespace Smart.NotificationCenter.Service
{
	public class Startup
	{
		public void BuildApplication(IAppBuilder appBuilder)
		{

		}
	}
}