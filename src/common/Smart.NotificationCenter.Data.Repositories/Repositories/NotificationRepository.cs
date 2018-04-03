using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Smart.NotificationCenter.Data.Abstractions;
using Smart.NotificationCenter.Data.Entities;
using Smart.NotificationCenter.Data.EntityFramework;

namespace Smart.NotificationCenter.Data.Repositories
{
	public class NotificationRepository : BaseRepository<Guid, Notification>, INotificationRepository, IRepository<Notification>
	{
		public NotificationRepository(IDbContextAccessor contextAccessor) : base(contextAccessor) { }
	}
}