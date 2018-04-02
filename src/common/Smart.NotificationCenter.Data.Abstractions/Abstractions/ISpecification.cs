using System;
using System.Linq.Expressions;

namespace Smart.NotificationCenter.Data.Abstractions
{
	public interface ISpecification<T>
	{
		Expression<Func<T, bool>> ToExpression();
	}
}