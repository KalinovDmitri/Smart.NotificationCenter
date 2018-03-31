using System;
using System.Collections.Generic;

namespace Smart.NotificationCenter.Data.Entities
{
	public class Role
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public bool Available { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public virtual ICollection<User> Users { get; set; }

		public virtual ICollection<Notification> Notifications { get; set; }
	}
}