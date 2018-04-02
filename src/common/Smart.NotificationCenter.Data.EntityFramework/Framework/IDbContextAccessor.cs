using System;
using System.Data.Entity;

namespace Smart.NotificationCenter.Data.EntityFramework
{
	public interface IDbContextAccessor
	{
		DbContext GetContext();
	}
}