using System;

namespace Smart.NotificationCenter.Service.Dtos
{
	public class NotificationDto
	{
		public string Title { get; set; }

		public string Body { get; set; }

		public Guid RoleId { get; set; }

		public NotificationSettings Settings { get; set; }
	}
}