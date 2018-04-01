using System;
using System.Web.Http;

using Unity;
using Unity.AspNet.WebApi;

using WebActivatorEx;

using Smart.NotificationCenter.Service;

[assembly: PreApplicationStartMethod(typeof(ContainerActivator), nameof(ContainerActivator.Start), Order = 1)]

namespace Smart.NotificationCenter.Service
{
	public class ContainerActivator
	{
		public static void Start()
		{
			var configuration = new HttpConfiguration();


		}
	}
}