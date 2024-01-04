using Godot;

namespace DemonCastle.Editor.Editors.States.Editor.Events.Actions;

public partial class EventActionList : ItemList {
	public EventActionList() {
		Name = nameof(EventActionList);

		AddChild(new Label{ Text = "face(player)"});
	}
}