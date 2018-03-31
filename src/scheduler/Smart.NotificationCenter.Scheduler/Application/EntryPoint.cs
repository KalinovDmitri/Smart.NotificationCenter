using System;

using NLog;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.ServiceConfigurators;
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
			// TODO: implement running for Quartz scheduler and CrystalReports service running
		}
	}
}