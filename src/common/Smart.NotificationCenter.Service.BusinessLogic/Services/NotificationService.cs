using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Quartz;
using Quartz.Core;
using Quartz.Impl;

using Smart.NotificationCenter.Data.Entities;
using Smart.NotificationCenter.Data.Abstractions;
using Smart.NotificationCenter.Data.EntityFramework;
using Smart.NotificationCenter.Data.Repositories;
using Smart.NotificationCenter.Jobs;
using Smart.NotificationCenter.Service.Dtos;

namespace Smart.NotificationCenter.Service.BusinessLogic
{
	public class NotificationService : ApplicationServiceBase, INotificationService
	{
		private readonly INotificationRepository _notificationRepository;
		private readonly IJobFactory _jobFactory;
		private readonly IJobScheduleService _jobScheduleService;

		public NotificationService(IUnitOfWork unitOfWork,
			INotificationRepository notificationRepository,
			IJobFactory jobFactory,
			IJobScheduleService jobScheduleService) : base(unitOfWork)
		{
			_notificationRepository = notificationRepository;
			_jobFactory = jobFactory;
			_jobScheduleService = jobScheduleService;
		}

		public async Task<IdentityDto<Guid>> CreateCustomNotificationAsync(NotificationDto notificationDto)
		{
			try
			{
				var newNotification = await _unitOfWork.ExecuteAsync(CreateCustomNotificationFromDto, notificationDto);

				try
				{
					var jobInfo = _jobFactory.CreateJob<CustomNotificationJob>(notificationDto, newNotification.Id, "custom");

					_jobScheduleService.ScheduleJob(jobInfo);

					string jobKey = jobInfo.Job.Key.ToString();
					newNotification.JobKey = jobKey;

					await _unitOfWork.SaveChangesAsync();
				}
				catch
				{
					await _unitOfWork.ExecuteAsync((Notification existingNotification) =>
					{
						return _notificationRepository.Remove(existingNotification);
					}, newNotification);

					throw;
				}

				return new IdentityDto<Guid>
				{
					Id = newNotification.Id
				};
			}
			catch
			{
				throw;
			}
		}

		private Notification CreateCustomNotificationFromDto(NotificationDto notificationDto)
		{
			return _notificationRepository.Add(new Notification
			{
				Title = notificationDto.Title,
				Body = notificationDto.Body,
				IsEnabled = true,
				JobKey = "",
				RoleId = notificationDto.RoleId,
				Type = NotificationType.Custom,
				SendingType = Data.Entities.NotificationSendingType.Email
			});
		}
	}
}