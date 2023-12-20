using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using DemonCastle.Editor.Editors.Properties.Rect;
using DemonCastle.Editor.Editors.Properties.Reference;
using DemonCastle.Editor.Editors.Properties.Vector;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;
using Vector2IProperty = DemonCastle.Editor.Editors.Properties.Vector.Vector2IProperty;

namespace DemonCastle.Editor.Editors.Properties;

public partial class PropertyCollection : BoxContainer, IBaseProperty {
	public virtual string DisplayName { get; set; } = string.Empty;

	public void Enable() {
		foreach (var child in GetChildren().Where(c => c is IBaseProperty).Cast<IBaseProperty>()) {
			child.Enable();
		}
	}

	public void Disable() {
		foreach (var child in GetChildren().Where(c => c is IBaseProperty).Cast<IBaseProperty>()) {
			child.Disable();
		}
	}

	public PropertyCollection() {
		Name = nameof(PropertyCollection);
		Vertical = true;
	}

	public FileProperty AddFile<T>(string name, T target, string directory,
						   Expression<Func<T, string>> propertyExpression,
						   IEnumerable<IFileType> fileTypes) where T : INotifyPropertyChanged {
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

	public Vector2IProperty AddVector2I<T>(string name, T target, Expression<Func<T, Vector2I>> propertyExpression, Vector2IPropertyOptions? options = null) where T : INotifyPropertyChanged {
		var vector2IProperty = new Vector2IProperty(new PropertyBinding<T, Vector2I>(target, propertyExpression), options ?? new Vector2IPropertyOptions()) {
			DisplayName = name
		};
		AddChild(vector2IProperty);
		return vector2IProperty;
	}

	public Vector2Property AddVector2<T>(string name, T target, Expression<Func<T, Vector2>> propertyExpression, Vector2PropertyOptions? options = null) where T : INotifyPropertyChanged {
		var vector2Property = new Vector2Property(new PropertyBinding<T, Vector2>(target, propertyExpression), options ?? new Vector2PropertyOptions()) {
			DisplayName = name
		};
		AddChild(vector2Property);
		return vector2Property;
	}

	public Rect2IProperty AddRect2I<T>(string name, T target, Expression<Func<T, Rect2I>> propertyExpression) where T : INotifyPropertyChanged {
		var rect2IProperty = new Rect2IProperty(new PropertyBinding<T, Rect2I>(target, propertyExpression)) {
			DisplayName = name
		};
		AddChild(rect2IProperty);
		return rect2IProperty;
	}

	public NullableRect2IProperty AddNullableRect2I<T>(string name, T target, Expression<Func<T, Rect2I?>> propertyExpression) where T : INotifyPropertyChanged {
		var nullableRect2IProperty = new NullableRect2IProperty(new PropertyBinding<T, Rect2I?>(target, propertyExpression)) {
			DisplayName = name
		};
		AddChild(nullableRect2IProperty);
		return nullableRect2IProperty;
	}

	public SpriteReferenceProperty AddSpriteReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerable<ISpriteDefinition> options) where T : INotifyPropertyChanged {
		var spriteReferenceProperty = new SpriteReferenceProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(spriteReferenceProperty);
		return spriteReferenceProperty;
	}

	public StateReferenceProperty AddStateReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerable<StateInfo> options) where T : INotifyPropertyChanged {
		var stateReferenceProperty = new StateReferenceProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(stateReferenceProperty);
		return stateReferenceProperty;
	}

	public AreaReferenceProperty AddAreaReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerable<AreaInfo> options) where T : INotifyPropertyChanged {
		var areaReferenceProperty = new AreaReferenceProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(areaReferenceProperty);
		return areaReferenceProperty;
	}

	public MonsterReferenceProperty AddMonsterReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerable<MonsterInfo> options) where T : INotifyPropertyChanged {
		var monsterReferenceProperty = new MonsterReferenceProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(monsterReferenceProperty);
		return monsterReferenceProperty;
	}

	public void AddAnimationReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerableInfo<IAnimationInfo> options) where T : INotifyPropertyChanged {
		AddChild(new AnimationNameProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		});
	}

	public void AddAnchor<T>(string name, T target, Expression<Func<T, Vector2I>> propertyExpression) where T : INotifyPropertyChanged {
		AddChild(new Properties.Anchor.AnchorProperty(new PropertyBinding<T, Vector2I>(target, propertyExpression)) {
			DisplayName = name
		});
	}

	public void AddOrigin<T>(string name, T target, Expression<Func<T, Vector2I>> anchorExpression, Expression<Func<T, Vector2I>> offsetExpression) where T : INotifyPropertyChanged {
		var group = new NamedPropertyCollection {
			DisplayName = name,
			Vertical = false
		};
		group.AddAnchor("Anchor", target, anchorExpression);
		var offset = group.AddVector2I("Offset", target, offsetExpression, new Vector2IPropertyOptions { AllowNegative = true, Vertical = true });
		offset.SizeFlagsHorizontal = SizeFlags.ExpandFill;
		AddChild(group);
	}
}