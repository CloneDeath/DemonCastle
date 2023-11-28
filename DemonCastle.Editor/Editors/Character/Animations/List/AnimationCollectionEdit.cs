using System;
using System.Linq;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations.List;

public partial class AnimationCollectionEdit : VBoxContainer {
	protected AnimationInfoCollection Animations { get; }
	protected AnimationItemList AnimationItems { get; }
	protected Button AddButton { get; }
	protected Button RemoveButton { get; }

	public event Action<AnimationInfo>? AnimationSelected;

	public AnimationCollectionEdit(AnimationInfoCollection animations) {
		Animations = animations;

		AddChild(AddButton = new Button {
			Text = "Add"
		});
		AddButton.Pressed += OnAddPressed;

		AddChild(AnimationItems = new AnimationItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		AnimationItems.ItemSelected += OnAnimationSelected;
		AnimationItems.AddAnimations(animations);

		AddChild(RemoveButton = new Button {
			Text = "Remove"
		});
		RemoveButton.Pressed += OnRemovePressed;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		RemoveButton.Disabled = !AnimationItems.IsAnythingSelected();
	}

	protected void OnAnimationSelected(long index) {
		var animationInfo = Animations[(int)index];
		AnimationSelected?.Invoke(animationInfo);
	}

	protected void OnAddPressed() {
		var animationInfo = Animations.AppendNew();
		AnimationItems.AddAnimationInfo(animationInfo);
	}

	protected void OnRemovePressed() {
		var animationIndex = AnimationItems.GetSelectedItems()[0];
		Animations.RemoveAt(animationIndex);
		AnimationItems.RemoveItem(animationIndex);
	}
}