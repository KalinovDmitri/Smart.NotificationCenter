using System;

namespace Smart.NotificationCenter.Data.Entities
{
	public abstract class BaseEntity<TKey> : AbstractEntity where TKey : struct
	{
		public TKey Id { get; set; }
	}
}