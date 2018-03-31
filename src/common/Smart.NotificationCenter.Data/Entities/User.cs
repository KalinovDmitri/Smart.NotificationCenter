using System;
using System.Collections.Generic;

namespace Smart.NotificationCenter.Data.Entities
{
	public class User
	{
		public Guid Id { get; set; }

		public string AccountName { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public bool LockedOut { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public virtual ICollection<Role> Roles { get; set; }
	}
}