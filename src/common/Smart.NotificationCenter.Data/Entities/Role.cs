using System;
using System.Collections.Generic;

namespace Smart.NotificationCenter.Data.Entities
{
	public class Role : UpdatableEntity<Guid>
	{
		public string Name { get; set; }

		public bool Available { get; set; }

		public virtual ICollection<User> Users { get; set; }

		public virtual ICollection<Notification> Notifications { get; set; }
	}
}