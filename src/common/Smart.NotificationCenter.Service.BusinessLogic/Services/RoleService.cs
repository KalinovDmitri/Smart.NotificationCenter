using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Smart.NotificationCenter.Data.Entities;
using Smart.NotificationCenter.Data.Abstractions;
using Smart.NotificationCenter.Data.EntityFramework;
using Smart.NotificationCenter.Data.Repositories;
using Smart.NotificationCenter.Jobs;
using Smart.NotificationCenter.Service.Dtos;

namespace Smart.NotificationCenter.Service.BusinessLogic
{
	public class RoleService : ApplicationServiceBase, IRoleService
	{
		private readonly IRoleRepository _roleRepository;

		public RoleService(IUnitOfWork unitOfWork,
			IRoleRepository roleRepository) : base(unitOfWork)
		{
			_roleRepository = roleRepository;
		}

		public async Task<IdentityDto<Guid>> CreateRoleAsync(RoleDto role)
		{
			var newRole = await _unitOfWork.ExecuteAsync((RoleDto roleDto) =>
			{
				return _roleRepository.Add(new Role
				{
					Id = roleDto.Id,
					Name = roleDto.Name,
					Available = true
				});
			}, role);

			return new IdentityDto<Guid>
			{
				Id = newRole.Id
			};
		}

		public async Task<List<RoleDto>> GetRolesAsync()
		{
			var result = await _unitOfWork.ReturnAsync(async () =>
			{
				return await _roleRepository
					.Where(new Specification<Role>(x => x.Available))
					.Select(x => new RoleDto
					{
						Id = x.Id,
						Name = x.Name
					})
					.ToListAsync();
			});

			return result;
		}
	}
}