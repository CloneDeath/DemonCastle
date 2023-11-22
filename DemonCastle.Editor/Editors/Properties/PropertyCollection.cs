using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class PropertyCollection : VBoxContainer {
	public void EnableProperties() {
		foreach (var child in GetChildren().Where(c => c is IBaseProperty).Cast<IBaseProperty>()) {
			child.Enable();
		}
	}

	public void DisableProperties() {
		foreach (var child in GetChildren().Where(c => c is IBaseProperty).Cast<IBaseProperty>()) {
			child.Disable();
		}
	}

	public PropertyCollection() {
		Name = nameof(PropertyCollection);
	}

	public FileProperty AddFile<T>(string name, T target, string directory,
						   Expression<Func<T, string>> propertyExpression,
						   IEnumerable<IFileTypeData> fileTypes) where T : INotifyPropertyChanged {
		var fileProperty = new FileProperty(new PropertyBinding<T,string>(target, propertyExpression), directory, fileTypes) {
			DisplayName = name
		};
		AddChild(fileProperty);
		return fileProperty;
	}

	public void AddColor<T>(string name, T target,Expression<Func<T, Color>> propertyExpression) where T : INotifyPropertyChanged {
		AddChild(new ColorProperty(new PropertyBinding<T, Color>(target, propertyExpression)) {
			DisplayName = name
		});
	}

	public StringProperty AddString<T>(string name, T target, Expression<Func<T, string>> propertyExpression) where T : INotifyPropertyChanged {
		var stringProperty = new StringProperty(new PropertyBinding<T, string>(target, propertyExpression)) {
			DisplayName = name
		};
		AddChild(stringProperty);
		return stringProperty;
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

	public SpriteReferenceProperty AddSpriteReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerable<ISpriteDefinition> options) where T : INotifyPropertyChanged {
		var spriteReferenceProperty = new SpriteReferenceProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(spriteReferenceProperty);
		return spriteReferenceProperty;
	}

	public AreaReferenceProperty AddAreaReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerable<AreaInfo> options) where T : INotifyPropertyChanged {
		var areaReferenceProperty = new AreaReferenceProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(areaReferenceProperty);
		return areaReferenceProperty;
	}

	public void AddAnimationName<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerable<CharacterAnimationInfo> options) where T : INotifyPropertyChanged {
		AddChild(new AnimationNameProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		});
	}
}