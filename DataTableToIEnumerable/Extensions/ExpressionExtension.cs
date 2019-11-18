using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataTableToIEnumerable.Extensions
{
    public static class ExpressionExtension
    {
        #region ----- PUBLIC METHODS --------------------------------------------------

        public static string GetMemberName(this Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("Expression can't be null");
            }

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException("Expression is not handled");
        }

        public static string GetMemberName(this UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }

        #endregion
    }
}
