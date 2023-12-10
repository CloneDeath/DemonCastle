using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor;

public partial class StateEditor : VSplitContainer {
	private StateDetails Details { get; }

	public StateEditor(IFileInfo file) {
		Name = nameof(StateEditor);

		AddChild(Details = new StateDetails());
	}


	public void LoadState(StateInfo state) {
		Details.State = state;
	}
}