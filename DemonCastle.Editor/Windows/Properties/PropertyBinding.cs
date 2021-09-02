using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DemonCastle.Editor.Windows.Properties {
	public interface IPropertyBinding<TProperty> {
		TProperty Get();
		void Set(TProperty value);
	}
	public class PropertyBinding<TObject, TProperty> : IPropertyBinding<TProperty> {
		protected TObject Target { get; }
		protected Expression<Func<TObject, TProperty>> PropertyExpression { get; }

		protected MemberExpression Expression => (MemberExpression)PropertyExpression.Body;
		protected PropertyInfo Property => (PropertyInfo)Expression.Member;

		public PropertyBinding(TObject target, Expression<Func<TObject, TProperty>> propertyExpression) {
			Target = target;
			PropertyExpression = propertyExpression;
		}

		public TProperty Get() => (TProperty)Property.GetValue(Target);
		public void Set(TProperty value) => Property.SetValue(Target, value);
	}
}