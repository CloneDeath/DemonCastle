using DemonCastle.Editor.Editors.Components.States.Editor;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States;

public partial class StatesEditor : HSplitContainer {
	protected InfoCollectionEditor<EntityStateInfo> StateList { get; }
	protected StateEditor StateEditor { get; }

	public StatesEditor(ProjectInfo project) {
		Name = nameof(StatesEditor);

		AddChild(StateList = new InfoCollectionEditor<EntityStateInfo>{
			CustomMinimumSize = new Vector2(300, 300)
		});
		StateList.ItemSelected += StateList_OnItemSelected;

		AddChild(StateEditor = new StateEditor(project) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	public void Load(IBaseEntityInfo? entity) {
		StateList.Load(entity?.States);
	}

	protected void StateList_OnItemSelected(EntityStateInfo? stateInfo) {
		StateEditor.LoadState(stateInfo);
	}
}