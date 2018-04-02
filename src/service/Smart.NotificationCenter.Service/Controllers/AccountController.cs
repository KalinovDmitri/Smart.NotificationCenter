using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Smart.NotificationCenter.Service.Controllers
{
	[RoutePrefix("account")]
	public class AccountController : ApiController
	{
		public AccountController() : base() { }

		[Route("login")]
		[HttpPost]
		public async Task<IHttpActionResult> Login()
		{
			await Task.Delay(100);

			return Ok();
		}
	}
}