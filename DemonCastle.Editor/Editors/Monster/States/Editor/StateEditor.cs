using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor;

public partial class StateEditor : VSplitContainer {
	private StateDetails Details { get; }

	public StateEditor(IFileInfo file, IEnumerableInfo<IAnimationInfo> animations) {
		Name = nameof(StateEditor);

		AddChild(Details = new StateDetails(animations));
	}

	public void LoadState(StateInfo state) {
		Details.State = state;
	}
}