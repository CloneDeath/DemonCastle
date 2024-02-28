using DemonCastle.Editor.Editors.Components.Actions;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Item;

public partial class ItemEventsEditor : ActionCollectionEditor<ItemActionInfo> {
	public ItemEventsEditor(IEnumerableInfo<ItemActionInfo> actionSet) {
		Name = nameof(ItemEventsEditor);
		_actionSet = actionSet;
	}

	protected override void AddAction(ItemActionInfo action) {
		if (_actionSet == null) return;
		Actions.AddChild(new ItemActionEditor(_actionSet, action));
	}
}