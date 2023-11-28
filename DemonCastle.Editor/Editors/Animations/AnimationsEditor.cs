using DemonCastle.Editor.Editors.Animations.Editor;
using DemonCastle.Editor.Editors.Animations.List;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Animations;

public partial class AnimationsEditor : HSplitContainer {
	private AnimationList AnimationList { get; }
	private AnimationEditor AnimationEditor { get; }

	public AnimationsEditor(IEnumerableInfo<IAnimationInfo> animations) {
		Name = nameof(AnimationsEditor);

		AddChild(AnimationList = new AnimationList(animations) {
			CustomMinimumSize = new Vector2(300, 300)
		});
		AnimationList.AnimationSelected += AnimationList_OnAnimationSelected;

		AddChild(AnimationEditor = new AnimationEditor(animations) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	private void AnimationList_OnAnimationSelected(IAnimationInfo animation) {
		AnimationEditor.LoadAnimation(animation);
	}
}