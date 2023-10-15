using System;
using System.ComponentModel;
using System.Linq.Expressions;
using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class PropertyCollection : VBoxContainer {
	public PropertyCollection() {
		Name = nameof(PropertyCollection);
	}

	public void AddFile<T>(string name, T target, string directory,
						   Expression<Func<T, string>> propertyExpression) where T : INotifyPropertyChanged {
		AddChild(new FileProperty(new PropertyBinding<T,string>(target, propertyExpression), directory) {
			DisplayName = name
		});
	}

	public void AddColor<T>(string name, T target,Expression<Func<T, Color>> propertyExpression) where T : INotifyPropertyChanged {
		AddChild(new ColorProperty(new PropertyBinding<T, Color>(target, propertyExpression)) {
			DisplayName = name
		});
	}

	public void AddString<T>(string name, T target, Expression<Func<T, string>> propertyExpression) where T : INotifyPropertyChanged {
		AddChild(new StringProperty(new PropertyBinding<T, string>(target, propertyExpression)) {
			DisplayName = name
		});
	}

	public void AddInteger<T>(string name, T target, Expression<Func<T, int>> propertyExpression) where T : INotifyPropertyChanged {
		AddChild(new IntegerProperty(new PropertyBinding<T,int>(target, propertyExpression)) {
			DisplayName = name
		});
	}

	public void AddFloat<T>(string name, T target, Expression<Func<T, float>> propertyExpression) where T : INotifyPropertyChanged {
		AddChild(new FloatProperty(new PropertyBinding<T,float>(target, propertyExpression)) {
			DisplayName = name
		});
	}

	public void AddBoolean<T>(string name, T target, Expression<Func<T, bool>> propertyExpression) where T : INotifyPropertyChanged{
		AddChild(new BooleanProperty(new PropertyBinding<T,bool>(target, propertyExpression)) {
			DisplayName = name
		});
	}

	public void AddVector2I<T>(string name, T target, Expression<Func<T, Vector2I>> propertyExpression) where T : INotifyPropertyChanged {
		AddChild(new Vector2IProperty(new PropertyBinding<T, Vector2I>(target, propertyExpression)) {
			DisplayName = name
		});
	}

	public void AddRect2I<T>(string name, T target, Expression<Func<T, Rect2I>> propertyExpression) where T : INotifyPropertyChanged {
		AddChild(new Rect2IProperty(new PropertyBinding<T, Rect2I>(target, propertyExpression)) {
			DisplayName = name
		});
	}
}