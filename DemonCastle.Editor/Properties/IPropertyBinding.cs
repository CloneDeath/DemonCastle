namespace DemonCastle.Editor.Properties; 

public interface IPropertyBinding<TProperty> {
	TProperty Get();
	void Set(TProperty value);
}