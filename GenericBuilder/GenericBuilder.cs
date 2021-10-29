namespace GenericBuilder
{
    using System;
    using System.Linq.Expressions;

    //  Implementação abstrata do design pattern Builder
    public abstract class GenericBuilder<TResult> : IBuilder<TResult> where TResult : class, new()
    {
        readonly TResult _result = default;

        public virtual TResult Build()
        {
            return _result;
        }

        protected void AddPart<TValue>(Expression<Func<TResult, TValue>> memberSelector, Func<TValue> valueFunc) =>
            AddPart(memberSelector, valueFunc());

        // referência: https://stackoverflow.com/questions/9601707/how-to-set-property-value-using-expressions
        protected void AddPart<TValue>(Expression<Func<TResult, TValue>> memberSelector, TValue value)
        {
            if (memberSelector.Body is MemberExpression memberSelectorExpression)
            {
                string propertyPath = Utils.GetPropertyPath(memberSelectorExpression);
                Utils.SetPropertyValue(_result, propertyPath, value);
            }
        }

        protected GenericBuilder()
        {
            _result = Activator.CreateInstance<TResult>();
        }
    }
}