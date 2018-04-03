using System;
using System.Threading;
using System.Threading.Tasks;

using Smart.NotificationCenter.Data.Abstractions;
using Smart.NotificationCenter.Data.Entities;

namespace Smart.NotificationCenter.Data.Repositories
{
	public interface INotificationRepository : IRepository<Notification> { }
}