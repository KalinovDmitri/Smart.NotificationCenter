using System;
using System.Linq.Expressions;

namespace Smart.NotificationCenter.Data.Abstractions
{
	internal class ParameterReplacerVisitor : ExpressionVisitor
	{
		private readonly ParameterExpression _parameter;
		private readonly ParameterExpression _replacement;

		public ParameterReplacerVisitor(ParameterExpression parameter, ParameterExpression replacement)
		{
			_parameter = parameter;
			_replacement = replacement;
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			return base.VisitParameter(_parameter == node ? _replacement : node);
		}
	}
}