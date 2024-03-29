using System.Linq;
using DemonCastle.Editor.Editors.Components.Animations.Editor;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Animations;

public partial class AnimationsEditor : HSplitContainer {
	private InfoCollectionEditor<IAnimationInfo> AnimationList { get; }
	private AnimationEditor AnimationEditor { get; }

	public IEnumerableInfo<IAnimationInfo>? Animations {
		get => AnimationList.Items;
		set => AnimationList.Items = value;
	}

	public AnimationsEditor(IFileInfo file) {
		Name = nameof(AnimationsEditor);

		AddChild(AnimationList = new InfoCollectionEditor<IAnimationInfo> {
			CustomMinimumSize = new Vector2(300, 300)
		});
		AnimationList.ItemSelected += AnimationList_OnItemSelected;

		AddChild(AnimationEditor = new AnimationEditor(file) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	public void Load(IEnumerableInfo<IAnimationInfo>? animations) {
		var selected = AnimationList.SelectedItem;
		AnimationList.Load(animations);
		var newSelected = animations?.FirstOrDefault(a => a.Name == selected?.Name)
						  ?? animations?.FirstOrDefault(a => a.Name.ToLower() == "default")
						  ?? animations?.FirstOrDefault();
		AnimationList.SelectedItem = newSelected;
		AnimationEditor.LoadAnimation(newSelected);
	}

	private void AnimationList_OnItemSelected(IAnimationInfo? animation) {
		AnimationEditor.LoadAnimation(animation);
	}
}