using System;
using System.Web;

using Unity.Lifetime;

namespace Smart.NotificationCenter.DependencyInjection
{
	public class HttpContextLifetimeManager : LifetimeManager, IDisposable
	{
		private readonly Guid _key = Guid.NewGuid();

		public override object GetValue(ILifetimeContainer container = null)
		{
			var items = HttpContext.Current?.Items;
			if (items != null && items.Contains(_key))
			{
				return items[_key];
			}

			return null;
		}

		public override void SetValue(object newValue, ILifetimeContainer container = null)
		{
			var items = HttpContext.Current?.Items;
			if (items != null)
				items[_key] = newValue;
		}

		public override void RemoveValue(ILifetimeContainer container = null)
		{
			var items = HttpContext.Current?.Items;
			if (items != null)
				items.Remove(_key);
		}

		protected override LifetimeManager OnCreateLifetimeManager()
		{
			return new HttpContextLifetimeManager();
		}

		public void Dispose()
		{
			RemoveValue();
		}
	}
}