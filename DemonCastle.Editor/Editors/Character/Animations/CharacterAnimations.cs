using DemonCastle.Editor.Editors.Character.Animations.Editor;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations;

public partial class CharacterAnimations : HSplitContainer {
	protected InfoCollectionEditor<IAnimationInfo> AnimationList { get; }
	protected CharacterAnimationEditor AnimationEditor { get; }

	public CharacterAnimations(CharacterInfo character) {
		Name = nameof(CharacterAnimations);

		AddChild(AnimationList = new InfoCollectionEditor<IAnimationInfo>(character.Animations){
			CustomMinimumSize = new Vector2(300, 300)
		});
		AnimationList.ItemSelected += AnimationList_OnAnimationSelected;

		AddChild(AnimationEditor = new CharacterAnimationEditor(character) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	protected void AnimationList_OnAnimationSelected(IAnimationInfo? animationInfo) {
		AnimationEditor.LoadAnimation(animationInfo);
	}
}