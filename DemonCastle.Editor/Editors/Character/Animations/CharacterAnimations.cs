using DemonCastle.Editor.Editors.Character.Animations.Editor;
using DemonCastle.Editor.Editors.Character.Animations.List;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations;

public partial class CharacterAnimations : HSplitContainer {
	protected AnimationCollectionEdit AnimationList { get; }
	protected SingleAnimationEditArea AnimationEditor { get; }

	public CharacterAnimations(CharacterInfo characterInfo) {
		Name = nameof(CharacterAnimations);

		AddChild(AnimationList = new AnimationCollectionEdit(characterInfo.Animations){
			CustomMinimumSize = new Vector2(300, 300)
		});
		AnimationList.AnimationSelected += OnAnimationSelected;

		AddChild(AnimationEditor = new SingleAnimationEditArea {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	protected void OnAnimationSelected(CharacterAnimationInfo animationInfo) {
		AnimationEditor.CurrentAnimation = animationInfo;
	}
}