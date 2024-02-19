using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using DemonCastle.Editor.Editors.Components.Properties.Anchor;
using DemonCastle.Editor.Editors.Components.Properties.Color;
using DemonCastle.Editor.Editors.Components.Properties.File;
using DemonCastle.Editor.Editors.Components.Properties.Rect;
using DemonCastle.Editor.Editors.Components.Properties.Reference;
using DemonCastle.Editor.Editors.Components.Properties.Vector;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties;

public partial class PropertyCollection : BoxContainer, IBaseProperty {
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

	public AnimationNameProperty AddAnimationReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerableInfo<IAnimationInfo> options) where T : INotifyPropertyChanged {
		var animationNameProperty = new AnimationNameProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(animationNameProperty);
		return animationNameProperty;
	}

	public void AddAnchor<T>(string name, T target, Expression<Func<T, Vector2I>> propertyExpression) where T : INotifyPropertyChanged {
		AddChild(new AnchorProperty(new PropertyBinding<T, Vector2I>(target, propertyExpression)) {
			DisplayName = name
		});
	}

	public AreaReferenceProperty AddAreaReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerableInfo<AreaInfo> options) where T : INotifyPropertyChanged {
		var areaReferenceProperty = new AreaReferenceProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(areaReferenceProperty);
		return areaReferenceProperty;
	}

	public void AddBoolean<T>(string name, T target, Expression<Func<T, bool>> propertyExpression) where T : INotifyPropertyChanged{
		AddChild(new BooleanProperty(new PropertyBinding<T,bool>(target, propertyExpression)) {
			DisplayName = name
		});
	}

	public ColorProperty AddColor<T>(string name, T target,Expression<Func<T, Godot.Color>> propertyExpression, ColorPropertyOptions? options = null) where T : INotifyPropertyChanged {
		var colorProperty = new ColorProperty(new PropertyBinding<T, Godot.Color>(target, propertyExpression), options ?? new ColorPropertyOptions()) {
			DisplayName = name
		};
		AddChild(colorProperty);
		return colorProperty;
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

	public NullableFileProperty AddNullableFile<T>(string name, T target, string directory,
												   Expression<Func<T, string?>> propertyExpression,
												   IEnumerable<IFileType> fileTypes) where T : INotifyPropertyChanged {
		var fileProperty = new NullableFileProperty(new PropertyBinding<T,string?>(target, propertyExpression), directory, fileTypes) {
			DisplayName = name
		};
		AddChild(fileProperty);
		return fileProperty;
	}

	public EnumProperty<TEnum> AddEnum<T, TEnum>(string name, T target, Expression<Func<T, TEnum>> propertyExpression) where T : INotifyPropertyChanged where TEnum : struct, Enum {
		var enumProperty = new EnumProperty<TEnum>(new PropertyBinding<T, TEnum>(target, propertyExpression)) {
			DisplayName = name
		};
		AddChild(enumProperty);
		return enumProperty;
	}

	public void AddFloat<T>(string name, T target, Expression<Func<T, float>> propertyExpression) where T : INotifyPropertyChanged {
		AddChild(new FloatProperty(new PropertyBinding<T,float>(target, propertyExpression)) {
			DisplayName = name
		});
	}

	public void AddInteger<T>(string name, T target, Expression<Func<T, int>> propertyExpression, IntegerPropertyOptions? options = null) where T : INotifyPropertyChanged {
		AddChild(new IntegerProperty(new PropertyBinding<T,int>(target, propertyExpression), options) {
			DisplayName = name
		});
	}

	public ItemReferenceProperty AddItemReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerableInfo<ItemInfo> options) where T : INotifyPropertyChanged {
		var itemReferenceProperty = new ItemReferenceProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(itemReferenceProperty);
		return itemReferenceProperty;
	}

	public MonsterReferenceProperty AddMonsterReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerableInfo<MonsterInfo> options) where T : INotifyPropertyChanged {
		var monsterReferenceProperty = new MonsterReferenceProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(monsterReferenceProperty);
		return monsterReferenceProperty;
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

	public Rect2IProperty AddRect2I<T>(string name, T target, Expression<Func<T, Rect2I>> propertyExpression, Rect2IPropertyOptions? options = null) where T : INotifyPropertyChanged {
		var rect2IProperty = new Rect2IProperty(new PropertyBinding<T, Rect2I>(target, propertyExpression), options ?? new Rect2IPropertyOptions()) {
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

	public SpriteReferenceProperty AddSpriteDefinition<T>(T target, string directory,
														  Expression<Func<T, string>> fileExpression,
														  Expression<Func<T, Guid>> spriteExpression,
														  Func<T, IEnumerableInfo<ISpriteDefinition>> getOptions)
		where T : INotifyPropertyChanged {
		var fileProperty = AddFile("Sprite File", target, directory, fileExpression, FileType.SpriteSources);
		var spriteProperty = AddSpriteReference("Sprite", target, spriteExpression, getOptions(target));
		fileProperty.FileSelected += _ => {
			spriteProperty.LoadOptions(getOptions(target));
		};

		target.PropertyChanged += TargetOnPropertyChanged;
		spriteProperty.TreeExiting += () => {
			target.PropertyChanged -= TargetOnPropertyChanged;
		};

		return spriteProperty;

		void TargetOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
			spriteProperty.LoadOptions(getOptions(target));
		}
	}

	public SpriteReferenceProperty AddSpriteReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression, IEnumerableInfo<ISpriteDefinition> options) where T : INotifyPropertyChanged {
		var spriteReferenceProperty = new SpriteReferenceProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(spriteReferenceProperty);
		return spriteReferenceProperty;
	}

	public StateReferenceProperty AddStateReference<T>(string name, T target, Expression<Func<T, Guid>> propertyExpression,
													   IEnumerableInfo<EntityStateInfo> options, InternalMode @internal = InternalMode.Disabled) where T : INotifyPropertyChanged {
		var stateReferenceProperty = new StateReferenceProperty(new PropertyBinding<T, Guid>(target, propertyExpression), options) {
			DisplayName = name
		};
		AddChild(stateReferenceProperty, @internal: @internal);
		return stateReferenceProperty;
	}

	public StringProperty AddString<T>(string name, T target, Expression<Func<T, string>> propertyExpression,
									   InternalMode @internal = InternalMode.Disabled) where T : INotifyPropertyChanged {
		var stringProperty = new StringProperty(new PropertyBinding<T, string>(target, propertyExpression)) {
			DisplayName = name
		};
		AddChild(stringProperty, @internal: @internal);
		return stringProperty;
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
}