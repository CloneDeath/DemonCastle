using DemonCastle.Editor.Editors.Animations.Editor;
using DemonCastle.Editor.Editors.Animations.List;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Animations;

public partial class AnimationsEditor : HSplitContainer {
	private AnimationList AnimationList { get; }
	private AnimationEditor AnimationEditor { get; }

	public AnimationsEditor(WeaponInfo weapon) {
		Name = nameof(AnimationsEditor);

		AddChild(AnimationList = new AnimationList(weapon) {
			CustomMinimumSize = new Vector2(300, 300)
		});
		AnimationList.AnimationSelected += AnimationList_OnAnimationSelected;

		AddChild(AnimationEditor = new AnimationEditor(weapon) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	private void AnimationList_OnAnimationSelected(AnimationInfo animation) {
		AnimationEditor.LoadAnimation(animation);
	}
}