using System;
using System.Web.Http;

using Unity;
using Unity.Lifetime;
using Unity.AspNet.WebApi;

using Smart.NotificationCenter.DependencyInjection;
using Smart.NotificationCenter.Data.EntityFramework;
using Smart.NotificationCenter.Data.Repositories;

using Smart.NotificationCenter.Service.BusinessLogic;

namespace Smart.NotificationCenter.Service
{
	internal class IoCConfig
	{
		public static IUnityContainer Instance { get; private set; }

		public static IUnityContainer Configure(HttpConfiguration configuration)
		{
			var container = new UnityContainer();
			
			container.RegisterInstance(configuration);

			container.RegisterType<SmartDbContext>(new HttpContextLifetimeManager());

			container.RegisterSingleton<IDbContextAccessor, DbContextAccessor<SmartDbContext>>();
			container.RegisterSingleton<IUnitOfWork, UnitOfWork<SmartDbContext>>();

			container.RegisterSingleton<INotificationRepository, NotificationRepository>();
			container.RegisterSingleton<IRoleRepository, RoleRepository>();

			container.RegisterSingleton<IJobFactory, DefaultJobFactory>();
			container.RegisterSingleton<IJobScheduleService, JobScheduleService>();

			container.RegisterSingleton<INotificationService, NotificationService>();
			container.RegisterSingleton<IRoleService, RoleService>();

			// TODO: register all types here!

			Instance = container;

			return container;
		}
	}
}