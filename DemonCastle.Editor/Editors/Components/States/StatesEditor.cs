using DemonCastle.Editor.Editors.Components.States.Editor;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States;

public partial class StatesEditor : HSplitContainer {
	protected EnumerableInfoList<EntityStateInfo> StateList { get; }
	protected StateEditor StateEditor { get; }

	public StatesEditor(ProjectInfo project, IBaseEntityInfo entity) {
		Name = nameof(StatesEditor);

		AddChild(StateList = new EnumerableInfoList<EntityStateInfo>(entity.States){
			CustomMinimumSize = new Vector2(300, 300)
		});
		StateList.ItemSelected += StateList_OnItemSelected;

		AddChild(StateEditor = new StateEditor(project, entity) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	protected void StateList_OnItemSelected(EntityStateInfo? stateInfo) {
		StateEditor.LoadState(stateInfo);
	}
}