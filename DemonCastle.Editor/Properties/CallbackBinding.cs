using System;

namespace DemonCastle.Editor.Properties;

public class CallbackBinding<TProperty> : IPropertyBinding<TProperty> {
	protected Func<TProperty> GetFunction { get; }
	protected Action<TProperty> SetFunction { get; }

	public CallbackBinding(Func<TProperty> get, Action<TProperty> set) {
		GetFunction = get;
		SetFunction = set;
	}

	public TProperty Get() => GetFunction.Invoke();
	public void Set(TProperty value) {
		SetFunction.Invoke(value);
		Changed?.Invoke(value);
	}

	public event Action<TProperty>? Changed;
}