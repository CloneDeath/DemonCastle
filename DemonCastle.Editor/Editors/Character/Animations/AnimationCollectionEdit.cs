using System;
using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations;

public partial class AnimationCollectionEdit : VBoxContainer {
	protected CharacterAnimationInfoCollection Animations { get; }
	protected AnimationItemList AnimationItems { get; }
	protected Button AddButton { get; }
	protected Button RemoveButton { get; }

	public event Action<CharacterAnimationInfo>? AnimationSelected;

	public AnimationCollectionEdit(CharacterAnimationInfoCollection animations) {
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
		var animationInfo = Animations.Add(new CharacterAnimationData {
			Name = "animation" + Animations.Count()
		});
		AnimationItems.AddAnimationLibrary(animationInfo);
	}

	protected void OnRemovePressed() {
		var animationIndex = AnimationItems.GetSelectedItems()[0];
		Animations.RemoveAt(animationIndex);
		AnimationItems.RemoveItem(animationIndex);
	}
}