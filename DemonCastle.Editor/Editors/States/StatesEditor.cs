using DemonCastle.Editor.Editors.States.Editor;
using DemonCastle.Editor.Editors.States.List;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.States;

public partial class StatesEditor : HSplitContainer {
	protected StateList StateList { get; }
	protected StateEditor StateEditor { get; }

	public StatesEditor(IEnumerableInfo<StateInfo> states, IEnumerableInfo<IAnimationInfo> animations) {
		Name = nameof(StatesEditor);

		AddChild(StateList = new StateList(states){
			CustomMinimumSize = new Vector2(300, 300)
		});
		StateList.StateSelected += StateList_OnStateSelected;

		AddChild(StateEditor = new StateEditor(animations, states) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	protected void StateList_OnStateSelected(StateInfo stateInfo) {
		StateEditor.LoadState(stateInfo);
	}
}