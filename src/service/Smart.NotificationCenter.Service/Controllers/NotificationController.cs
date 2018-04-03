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

		[HttpPost]
		[Route("custom")]
		[ResponseType(typeof(IdentityDto<Guid>))]
		public async Task<IHttpActionResult> CreateCustomNotification([FromBody] NotificationDto notificationDto)
		{
			var result = await _notificationService.CreateCustomNotificationAsync(notificationDto);

			return Json(result);
		}
	}
}