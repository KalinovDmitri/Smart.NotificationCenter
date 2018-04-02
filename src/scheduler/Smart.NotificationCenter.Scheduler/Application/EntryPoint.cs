using System;

using NLog;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.ServiceConfigurators;
using Topshelf.Unity;
using Unity;
using Unity.Container;

namespace Smart.NotificationCenter.Scheduler
{
	internal class EntryPoint
	{
		internal static int Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

			TopshelfExitCode exitCode = HostFactory.Run(ConfigureHost);
			return (int)exitCode;
		}

		private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
		{
			ILogger logger = LogManager.GetCurrentClassLogger();

			Exception exc = args.ExceptionObject as Exception;
			if (args.IsTerminating)
			{
				Environment.ExitCode = exc.HResult;
				logger.Fatal(exc, exc.Message);
			}
			else
			{
				logger.Error(exc, exc.Message);
			}
		}

		private static void ConfigureHost(HostConfigurator configurator)
		{
			IUnityContainer container = UnityContainerFactory.BuildContainer();

			configurator.SetServiceName("Smart.NotificationService");
			configurator.SetDisplayName("Smart Notification Service");
			configurator.SetDescription("Smart Notification Service based on Quartz.Net");

			configurator.UseNLog(LogManager.LogFactory);
			configurator.UseUnityContainer(container);

			configurator.ApplyCommandLine();

			configurator.Service<QuartzServiceHost>(ConfigureServiceHost);

			configurator.RunAsLocalSystem();
		}

		private static void ConfigureServiceHost(ServiceConfigurator<QuartzServiceHost> configurator)
		{
			configurator.ConstructUsingUnityContainer();

			configurator.WhenStarted(StartServiceHost);
			configurator.WhenStopped(StopServiceHost);
		}

		private static bool StartServiceHost(QuartzServiceHost host, HostControl hostControl)
		{
			return host.Start();
		}

		private static bool StopServiceHost(QuartzServiceHost host, HostControl hostControl)
		{
			return host.Stop();
		}
	}
}