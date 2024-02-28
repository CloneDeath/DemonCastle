using DemonCastle.Editor.Editors.Components.Actions;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Item;

public partial class ItemEventsEditor : ActionCollectionEditor<ItemActionInfo> {
	public ItemEventsEditor(IEnumerableInfo<ItemActionInfo> actionSet) {
		Name = nameof(ItemEventsEditor);
		_actionSet = actionSet;

		Label onPickup;
		AddChild(onPickup = new Label{Text = "On Pickup"});
		MoveChild(onPickup, 0);
	}

	protected override void AddAction(ItemActionInfo action) {
		if (_actionSet == null) return;
		Actions.AddChild(new ItemActionEditor(_actionSet, action));
	}
}