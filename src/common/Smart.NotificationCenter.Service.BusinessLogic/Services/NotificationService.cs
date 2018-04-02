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
	public class NotificationService : INotificationService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly INotificationRepository _notificationRepository;
		private readonly IJobFactory _jobFactory;
		private readonly IJobScheduleService _jobScheduleService;

		public NotificationService(IUnitOfWork unitOfWork,
			INotificationRepository notificationRepository,
			IJobFactory jobFactory,
			IJobScheduleService jobScheduleService)
		{
			_unitOfWork = unitOfWork;
			_notificationRepository = notificationRepository;
			_jobFactory = jobFactory;
			_jobScheduleService = jobScheduleService;
		}

		public async Task<IdentityDto<Guid>> CreateCustomNotificationAsync(NotificationDto notification)
		{
			var jobInfo = _jobFactory.CreateJob<ICustomNotification>(notification, "custom");
			
			try
			{
				var newNotification = await _unitOfWork.ExecuteAsync((NotificationDto dto) =>
				{
					return _notificationRepository.Add(new Notification
					{
						Title = dto.Title,
						Body = dto.Body,
						IsEnabled = true,
						JobKey = jobInfo.Job.Key.ToString(),
						RoleId = new Guid("387FA07A-7736-E811-BE00-74D435BBE466"),
						Type = NotificationType.Custom,
						CreatedAt = DateTime.UtcNow,
						CronExpression = "",
						SendingType = Data.Entities.NotificationSendingType.Email
					});
				}, notification);

				try
				{
					_jobScheduleService.ScheduleJob(jobInfo);
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

		private Notification CreateNotificationFromJobInfo(NotificationDto notificationDto)
		{
			var notification = new Notification
			{
				Title = notificationDto.Title,
				Body = notificationDto.Body,
				IsEnabled = true,
				
			};

			return notification;
		}
	}
}