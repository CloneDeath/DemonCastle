using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Events;

public partial class EventsEditor : VBoxContainer {
	private readonly StateInfoProxy _proxy = new();

	public EntityStateInfo? State {
		get => _proxy.Proxy;
		set {
			_proxy.Proxy = value;
			if (Events.IsAnythingSelected()) {
				Events_OnItemSelected(Events.GetSelectedItems()[0]);
			} else {
				ActionList.Clear();
			}
		}
	}

	public IBaseEntityInfo? Entity {
		get => ActionList.Entity;
		set => ActionList.Entity = value;
	}

	private ItemList Events { get; }
	private EntityActionCollectionEditor ActionList { get; }

	public EventsEditor(ProjectResources resources) {
		Name = nameof(EventsEditor);

		AddChild(Events = new ItemList {
			CustomMinimumSize = new Vector2(0, 90)
		});
		Events.AddItem("OnEnter");
		Events.AddItem("OnUpdate");
		Events.AddItem("OnExit");
		Events.ItemSelected += Events_OnItemSelected;

		AddChild(ActionList = new EntityActionCollectionEditor(resources) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
	}

	private void Events_OnItemSelected(long index) {
		if (State == null) {
			ActionList.Clear();
			return;
		}

		var actionSet = index switch {
			0 => State.OnEnter,
			1 => State.OnUpdate,
			2 => State.OnExit,
			_ => null
		};
		if (actionSet == null) return;

		ActionList.Load(actionSet);
	}
}