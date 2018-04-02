using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using Smart.NotificationCenter.Service.Dtos;
using Smart.NotificationCenter.Service.BusinessLogic;

namespace Smart.NotificationCenter.Service.Controllers
{
	[RoutePrefix("notifications")]
	public class NotificationController : ApiController
	{
		private INotificationService _notificationService;

		public NotificationController(INotificationService notificationService) : base()
		{
			_notificationService = notificationService;
		}

		[Route("custom")]
		[HttpPost]
		public async Task<IHttpActionResult> CreateCustomNotification([FromBody] NotificationDto notification)
		{
			await _notificationService.CreateCustomNotificationAsync(notification);

			return Ok();
		}
	}
}