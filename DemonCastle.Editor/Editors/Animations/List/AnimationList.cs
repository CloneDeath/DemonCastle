using System;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Animations.List;

public partial class AnimationList : VBoxContainer {
	public event Action<AnimationInfo>? AnimationSelected;

	private readonly WeaponInfo _weapon;
	private ItemList Animations { get; }
	private Button AddButton { get; }
	private Button RemoveButton { get; }

	public AnimationList(WeaponInfo weapon) {
		_weapon = weapon;

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
		var animation = _weapon.Animations[(int)index];
		AnimationSelected?.Invoke(animation);
	}

	public override void _EnterTree() {
		base._EnterTree();
		_weapon.Animations.CollectionChanged += Animations_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_weapon.Animations.CollectionChanged -= Animations_OnCollectionChanged;
	}

	private void AddButton_OnPressed() {
		_weapon.Animations.Add(new AnimationData {
			Name = "animation"
		});
	}

	private void RemoveButton_OnPressed() {
		var selected = Animations.GetSelectedItems();
		if (!selected.Any()) return;

		_weapon.Animations.RemoveAt(selected[0]);
	}

	private void Animations_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadAnimations();
	}

	private void ReloadAnimations() {
		Animations.Clear();

		foreach (var animation in _weapon.Animations) {
			Animations.AddItem(animation.Name);
		}
	}
}