using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using Smart.NotificationCenter.Service.BusinessLogic;
using Smart.NotificationCenter.Service.Dtos;

namespace Smart.NotificationCenter.Service.Controllers
{
	[RoutePrefix("roles")]
	public class RoleController : ApiController
	{
		private readonly IRoleService _roleService;

		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		[HttpGet]
		[Route("")]
		[ResponseType(typeof(List<RoleDto>))]
		public async Task<IHttpActionResult> GetRoles()
		{
			var result = await _roleService.GetRolesAsync();

			return Json(result);
		}

		[HttpPost]
		[Route("new")]
		[ResponseType(typeof(IdentityDto<Guid>))]
		public async Task<IHttpActionResult> CreateRole([FromBody] RoleDto roleDto)
		{
			var result = await _roleService.CreateRoleAsync(roleDto);

			return Json(result);
		}
	}
}