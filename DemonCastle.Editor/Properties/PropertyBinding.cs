using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace DemonCastle.Editor.Properties;

public class PropertyBinding<TObject, TProperty> : IPropertyBinding<TProperty>
	where TObject : INotifyPropertyChanged
{
	protected TObject Target { get; }
	protected Expression<Func<TObject, TProperty>> PropertyExpression { get; }

	protected MemberExpression Expression => (MemberExpression)PropertyExpression.Body;
	protected PropertyInfo Property => (PropertyInfo)Expression.Member;

	public PropertyBinding(TObject target, Expression<Func<TObject, TProperty>> propertyExpression) {
		Target = target;
		PropertyExpression = propertyExpression;
		Target.PropertyChanged += Target_OnPropertyChanged;
	}

	private static bool TPropertyIsNullable => Nullable.GetUnderlyingType(typeof(TProperty)) != null;

	public TProperty Get() {
		if (TPropertyIsNullable) {
			return (TProperty)Property.GetValue(Target)!; // it's allowed to be null!
		}
		return (TProperty)(Property.GetValue(Target) ?? throw new NullReferenceException());
	}

	public void Set(TProperty value) {
		if (Equals(Get(), value)) return;
		Property.SetValue(Target, value);
		Changed?.Invoke(value);
	}

	public event Action<TProperty>? Changed;

	private void Target_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != Property.Name) return;
		Changed?.Invoke(Get());
	}
}