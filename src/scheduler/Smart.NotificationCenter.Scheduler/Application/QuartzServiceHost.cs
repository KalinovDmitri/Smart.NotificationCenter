using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Owin;

using Unity;
using Unity.Container;
using NLog;
using Quartz;
using Quartz.Core;
using Quartz.Impl;

using CrystalQuartz.Application;
using CrystalQuartz.Core;
using CrystalQuartz.Owin;

namespace Smart.NotificationCenter.Scheduler
{
	internal class QuartzServiceHost
	{
		private readonly ILogger _logger;
		private readonly QuartzSchedulerFactory _schedulerFactory;

		private IScheduler _scheduler;
		private IDisposable _serviceInstance;

		public QuartzServiceHost(ILogger logger, QuartzSchedulerFactory schedulerFactory)
		{
			_logger = logger;
			_schedulerFactory = schedulerFactory;
		}

		public bool Start()
		{
			bool started = false;

			try
			{
				_scheduler = _schedulerFactory.CreateScheduler();

				var startOptions = CreateStartOptions();
				_serviceInstance = WebApp.Start(startOptions, BuildApplication);

				started = true;
			}
			catch (Exception exc)
			{
				_logger.Error(exc, exc.Message);
			}

			return started;
		}

		public bool Stop()
		{
			_serviceInstance?.Dispose();
			_scheduler?.Shutdown(true);
			return true;
		}

		private StartOptions CreateStartOptions()
		{
			var startOptions = new StartOptions
			{
				Port = 5000
			};

			startOptions.Urls.Add(string.Format("http://localhost:{0}/", startOptions.Port));
			startOptions.Urls.Add(string.Format("http://{0}:{1}/", Environment.MachineName, startOptions.Port));

			return startOptions;
		}

		private void BuildApplication(IAppBuilder appBuilder)
		{
			appBuilder.UseCrystalQuartz(new SimpleSchedulerProvider(_scheduler));
		}
	}
}