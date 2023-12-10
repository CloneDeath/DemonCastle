using DemonCastle.Editor.Editors.Monster.States.Editor;
using DemonCastle.Editor.Editors.Monster.States.List;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States;

public partial class StatesEditor : HSplitContainer {
	protected StateList StateList { get; }
	protected StateEditor StateEditor { get; }

	public StatesEditor(IFileInfo file, IEnumerableInfo<StateInfo> states) {
		Name = nameof(StatesEditor);

		AddChild(StateList = new StateList(states){
			CustomMinimumSize = new Vector2(300, 300)
		});
		StateList.StateSelected += StateList_OnStateSelected;

		AddChild(StateEditor = new StateEditor(file) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	protected void StateList_OnStateSelected(StateInfo stateInfo) {
		StateEditor.LoadState(stateInfo);
	}
}