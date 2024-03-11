using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Events;
using Godot;

namespace DemonCastle.Editor.Editors.Components.BaseEntity.Events;

public partial class EventsEditor : HSplitContainer {
	protected InfoCollectionEditor<EntityEventInfo> EventsList { get; }

	public EventsEditor() {
		Name = nameof(EventsEditor);

		AddChild(EventsList = new InfoCollectionEditor<EntityEventInfo>());
		EventsList.ItemSelected += EventList_OnEventSelected;
	}

	private void EventList_OnEventSelected(EntityEventInfo? entityEvent) {
	}

	public void Load(IBaseEntityInfo? entity) {
		EventsList.Load(entity?.Events);
	}
}