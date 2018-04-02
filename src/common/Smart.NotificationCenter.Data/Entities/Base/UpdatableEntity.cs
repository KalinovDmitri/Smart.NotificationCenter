using System;

namespace Smart.NotificationCenter.Data.Entities
{
	public abstract class UpdatableEntity<TKey> : CreatableEntity<TKey> where TKey : struct
	{
		public DateTime? UpdatedAt { get; set; }
	}
}