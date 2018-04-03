using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Smart.NotificationCenter.Service.Dtos;

namespace Smart.NotificationCenter.Service.BusinessLogic
{
	public interface IRoleService
	{
		Task<List<RoleDto>> GetRolesAsync();

		Task<IdentityDto<Guid>> CreateRoleAsync(RoleDto role);
	}
}