using System;

namespace Smart.NotificationCenter.Data.Entities
{
	public abstract class CreatableEntity<TKey> : BaseEntity<TKey> where TKey : struct
	{
		public DateTime CreatedAt { get; set; }
	}
}