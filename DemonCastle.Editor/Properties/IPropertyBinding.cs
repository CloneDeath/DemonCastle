using System;

namespace DemonCastle.Editor.Properties;

public interface IPropertyBinding<TProperty> {
	TProperty Get();
	void Set(TProperty value);
	event Action<TProperty> Changed;
}