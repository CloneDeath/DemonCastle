using DemonCastle.Editor.Editors.Components.Animations.Editor;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Animations;

public partial class AnimationsEditor : HSplitContainer {
	private EnumerableInfoList<IAnimationInfo> AnimationList { get; }
	private AnimationEditor AnimationEditor { get; }

	public AnimationsEditor(IFileInfo file, IEnumerableInfo<IAnimationInfo> animations) {
		Name = nameof(AnimationsEditor);

		AddChild(AnimationList = new EnumerableInfoList<IAnimationInfo>(animations) {
			CustomMinimumSize = new Vector2(300, 300)
		});
		AnimationList.ItemSelected += AnimationList_OnItemSelected;

		AddChild(AnimationEditor = new AnimationEditor(file) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	private void AnimationList_OnItemSelected(IAnimationInfo? animation) {
		AnimationEditor.LoadAnimation(animation);
	}
}