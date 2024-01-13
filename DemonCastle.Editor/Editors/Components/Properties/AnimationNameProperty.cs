using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties;

public partial class AnimationNameProperty : BaseProperty {
	private readonly List<IAnimationInfo> _boundItems = new();
	private readonly IEnumerableInfo<IAnimationInfo> _options;

	private List<IAnimationInfo> Options => _options.ToList();

	protected IPropertyBinding<Guid> Binding { get; }
	protected OptionButton OptionButton { get; }

	public Guid PropertyValue {
		get => OptionButton.Selected < 0 ? Guid.Empty : Options[OptionButton.Selected].Id;
		set {
			var option = _options.FirstOrDefault(o => o.Id == value);
			OptionButton.Selected = option == null ? -1 : Options.IndexOf(option);
		}
	}

	public AnimationNameProperty(IPropertyBinding<Guid> binding, IEnumerableInfo<IAnimationInfo> options) {
		_options = options;
		Name = nameof(AnimationNameProperty);
		Binding = binding;

		AddChild(OptionButton = new OptionButton {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		ReloadOptions();
		PropertyValue = binding.Get();

		OptionButton.ItemSelected += OnItemSelected;
	}

	private void ReloadOptions() {
		foreach (var item in _boundItems) {
			item.PropertyChanged -= Option_OnPropertyChanged;
		}
		_boundItems.Clear();
		OptionButton.Clear();

		for (var i = 0; i < Options.Count; i++) {
			var option = Options[i];
			OptionButton.AddItem(option.Name, i);

			var spriteDefinition = option.Frames.FirstOrDefault()?.SpriteDefinition;
			if (spriteDefinition != null) {
				OptionButton.SetItemIcon(i, spriteDefinition.ToTexture());
			}

			_boundItems.Add(option);
			option.PropertyChanged += Option_OnPropertyChanged;
		}
	}

	private void Option_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		var selected = OptionButton.Selected;
		ReloadOptions();
		OptionButton.Selected = selected;
	}

	private void Options_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		var selected = OptionButton.Selected;
		ReloadOptions();
		OptionButton.Selected = selected;
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
		_options.CollectionChanged += Options_OnCollectionChanged;
		ReloadOptions();
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
		_options.CollectionChanged -= Options_OnCollectionChanged;

		foreach (var item in _boundItems) {
			item.PropertyChanged -= Option_OnPropertyChanged;
		}
		_boundItems.Clear();
	}

	private void Binding_OnChanged(Guid value) {
		PropertyValue = value;
	}

	private void OnItemSelected(long index) {
		Binding.Set(index < 0 ? Guid.Empty : Options[(int)index].Id);
	}

	public override void Enable() {
		base.Enable();
		OptionButton.Disabled = false;
	}

	public override void Disable() {
		base.Disable();
		OptionButton.Disabled = true;
	}
}