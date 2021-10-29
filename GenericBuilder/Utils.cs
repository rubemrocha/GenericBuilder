namespace GenericBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;

    internal static class Utils
    {
        public static void SetPropertyValue<TValue>(object target, string propertyPath, TValue value)
        {
            string[] nameParts = propertyPath.Split('.');
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
            if (nameParts.Length == 1)
                target.GetType().GetProperty(propertyPath, bindingFlags).SetValue(target, value, null);
            else
            {
                object reference = target;
                foreach (string part in nameParts)
                {
                    if (reference == null) return;
                    Type referenceType = reference.GetType();
                    PropertyInfo info = referenceType.GetProperty(part, bindingFlags);
                    if (info == null) continue;
                    if (!part.Equals(nameParts.Last()))
                        reference = info.GetValue(reference, null);
                    else
                        info.SetValue(reference, value, null);
                }
            }
        }

        // referência: https://www.codeproject.com/Articles/733296/Expression-Parsing-and-Nested-Properties-2
        public static MemberExpression GetMemberExpression(Expression expression)
        {
            if (expression is MemberExpression memberExpression)
                return memberExpression;

            if (expression is LambdaExpression)
            {
                LambdaExpression lambdaExpression = expression as LambdaExpression;

                if (lambdaExpression.Body is MemberExpression bodyLambdaExpression)
                    return bodyLambdaExpression;

                if (lambdaExpression.Body is UnaryExpression unaryExpression)
                    return ((MemberExpression)unaryExpression.Operand);
            }

            return default;
        }

        //  referência: https://www.codeproject.com/Articles/733296/Expression-Parsing-and-Nested-Properties-2
        public static string GetPropertyPath(Expression expr)
        {
            var path = new StringBuilder();
            MemberExpression memberExpression = GetMemberExpression(expr);
            do
            {
                if (path.Length > 0) path.Insert(0, ".");
                path.Insert(0, memberExpression.Member.Name);
                memberExpression = GetMemberExpression(memberExpression.Expression);
            }
            while (memberExpression != null);
            return path.ToString();
        }
    }
}