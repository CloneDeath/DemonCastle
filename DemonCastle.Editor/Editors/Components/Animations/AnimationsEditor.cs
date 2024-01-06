using DemonCastle.Editor.Editors.Components.Animations.Editor;
using DemonCastle.Editor.Editors.Components.Animations.List;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Animations;

public partial class AnimationsEditor : HSplitContainer {
	private AnimationList AnimationList { get; }
	private AnimationEditor AnimationEditor { get; }

	public AnimationsEditor(IFileInfo file, IEnumerableInfo<IAnimationInfo> animations) {
		Name = nameof(AnimationsEditor);

		AddChild(AnimationList = new AnimationList(animations) {
			CustomMinimumSize = new Vector2(300, 300)
		});
		AnimationList.AnimationSelected += AnimationList_OnAnimationSelected;

		AddChild(AnimationEditor = new AnimationEditor(file) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	private void AnimationList_OnAnimationSelected(IAnimationInfo animation) {
		AnimationEditor.LoadAnimation(animation);
	}
}