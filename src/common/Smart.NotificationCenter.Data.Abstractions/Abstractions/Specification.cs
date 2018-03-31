using System;
using System.Linq.Expressions;

namespace Smart.NotificationCenter.Data.Abstractions
{
	public class Specification<T> : ISpecification<T>
	{
		private Expression<Func<T, bool>> _predicate;

		public Specification(Expression<Func<T, bool>> predicate)
		{
			_predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
		}

		public Expression<Func<T, bool>> ToExpression() => _predicate;

		public static implicit operator Expression<Func<T, bool>>(Specification<T> spec)
		{
			return spec._predicate;
		}

		public static bool operator true(Specification<T> spec) => false;

		public static bool operator false(Specification<T> spec) => false;

		public static Specification<T> operator !(Specification<T> spec)
		{
			return new Specification<T>(
				Expression.Lambda<Func<T, bool>>(
					Expression.Not(spec._predicate.Body),
					spec._predicate.Parameters));
		}

		public static Specification<T> operator &(Specification<T> left, Specification<T> right)
		{
			var leftExpression = left._predicate;
			var rightExpression = right._predicate;
			var leftParam = leftExpression.Parameters[0];
			var rightParam = rightExpression.Parameters[0];

			return new Specification<T>(
				Expression.Lambda<Func<T, bool>>(
					Expression.AndAlso(
						leftExpression.Body,
						new ParameterReplacerVisitor(rightParam, leftParam).Visit(rightExpression.Body)),
					leftParam));
		}

		public static Specification<T> operator |(Specification<T> left, Specification<T> right)
		{
			var leftExpression = left._predicate;
			var rightExpression = right._predicate;
			var leftParam = leftExpression.Parameters[0];
			var rightParam = rightExpression.Parameters[0];

			return new Specification<T>(
				Expression.Lambda<Func<T, bool>>(
					Expression.OrElse(
						leftExpression.Body,
						new ParameterReplacerVisitor(rightParam, leftParam).Visit(rightExpression.Body)),
					leftParam));
		}
	}
}