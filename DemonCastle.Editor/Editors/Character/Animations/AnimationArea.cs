using DemonCastle.Editor.Editors.Character.Animations.Editor;
using DemonCastle.Editor.Editors.Character.Animations.List;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations;

public partial class AnimationArea : SplitContainer {
	protected AnimationCollectionEdit AnimationList { get; }
	protected SingleAnimationEditArea AnimationEdit { get; }

	public AnimationArea(CharacterInfo characterInfo) {
		SplitOffset = 200;

		AddChild(AnimationList = new AnimationCollectionEdit(characterInfo.Animations));
		AnimationList.AnimationSelected += OnAnimationSelected;

		AddChild(AnimationEdit = new SingleAnimationEditArea());
	}

	protected void OnAnimationSelected(CharacterAnimationInfo animationInfo) {
		AnimationEdit.CurrentAnimation = animationInfo;
	}
}