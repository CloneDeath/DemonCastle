using System;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Animations.List;

public partial class AnimationList : VBoxContainer {
	public event Action<IAnimationInfo>? AnimationSelected;

	private readonly IEnumerableInfo<IAnimationInfo> _animations;
	private Button AddButton { get; }
	private ItemList Animations { get; }
	private Button RemoveButton { get; }

	public AnimationList(IEnumerableInfo<IAnimationInfo> animations) {
		_animations = animations;

		Name = nameof(AnimationList);

		AddChild(AddButton = new Button { Text = "Add" });
		AddButton.Pressed += AddButton_OnPressed;
		AddChild(Animations = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		Animations.ItemSelected += Animations_OnItemSelected;
		AddChild(RemoveButton = new Button { Text = "Remove" });
		RemoveButton.Pressed += RemoveButton_OnPressed;

		ReloadAnimations();
	}

	private void Animations_OnItemSelected(long index) {
		var animation = _animations[(int)index];
		AnimationSelected?.Invoke(animation);
	}

	public override void _EnterTree() {
		base._EnterTree();
		_animations.CollectionChanged += Animations_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_animations.CollectionChanged -= Animations_OnCollectionChanged;
	}

	private void AddButton_OnPressed() {
		_animations.AppendNew();
	}

	private void RemoveButton_OnPressed() {
		var selected = Animations.GetSelectedItems();
		if (!selected.Any()) return;

		_animations.RemoveAt(selected[0]);
	}

	private void Animations_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadAnimations();
	}

	private void ReloadAnimations() {
		Animations.Clear();

		foreach (var animation in _animations) {
			Animations.AddItem(animation.Name);
		}
	}
}