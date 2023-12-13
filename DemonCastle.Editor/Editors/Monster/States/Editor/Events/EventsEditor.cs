using DemonCastle.Editor.Editors.Monster.States.Editor.Events.Actions;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor.Events;

public partial class EventsEditor : VBoxContainer {
	private readonly StateInfoProxy _proxy = new();

	public StateInfo? State {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	private ItemList Events { get; }

	public EventsEditor() {
		Name = nameof(EventsEditor);

		AddChild(Events = new ItemList {
			CustomMinimumSize = new Vector2(0, 90)
		});
		Events.AddItem("OnEnter");
		Events.AddItem("OnUpdate");
		Events.AddItem("OnExit");

		AddChild(new EventActionList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
	}
}