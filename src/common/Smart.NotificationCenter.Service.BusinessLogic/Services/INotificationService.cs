using System;
using System.Threading;
using System.Threading.Tasks;

using Smart.NotificationCenter.Service.Dtos;

namespace Smart.NotificationCenter.Service.BusinessLogic
{
	public interface INotificationService
	{
		Task<IdentityDto<Guid>> CreateCustomNotificationAsync(NotificationDto notification);
	}
}