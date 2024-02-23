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

		if (Property.SetMethod == null) throw new Exception("Property must have a setter.");
	}

	public TProperty Get() {
		return (TProperty)Property.GetValue(Target)!;
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