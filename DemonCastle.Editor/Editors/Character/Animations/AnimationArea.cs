using DemonCastle.Editor.Editors.Character.Animations.Editor;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations;

public partial class AnimationArea : Control {
	protected List.AnimationCollectionEdit Animations { get; }
	protected SplitContainer SplitContainer { get; }
	protected SingleAnimationEditArea AnimationEdit { get; }
	public AnimationArea(CharacterInfo characterInfo) {
		AddChild(SplitContainer = new HSplitContainer {
			AnchorRight = 1,
			OffsetRight = 0,
			AnchorBottom = 1,
			OffsetBottom = 0
		});
		SplitContainer.AddChild(Animations = new List.AnimationCollectionEdit(characterInfo.Animations));
		Animations.AnimationSelected += OnAnimationSelected;

		SplitContainer.AddChild(AnimationEdit = new SingleAnimationEditArea());
		SplitContainer.SplitOffset = 100;
	}

	protected void OnAnimationSelected(CharacterAnimationInfo animationInfo) {
		AnimationEdit.CurrentAnimation = animationInfo;
	}
}