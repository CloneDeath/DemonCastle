using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Events;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Components.BaseEntity.Events;

public partial class EventsEditor : HSplitContainer {
	private readonly ProjectResources _resources;

	protected InfoCollectionEditor<EntityEventInfo> EventsList { get; }
	protected MarginContainer RightPanel { get; }

	private IBaseEntityInfo? _entity;

	public EventsEditor(ProjectResources resources) {
		_resources = resources;
		Name = nameof(EventsEditor);

		AddChild(EventsList = new InfoCollectionEditor<EntityEventInfo>());
		EventsList.ItemSelected += EventList_OnEventSelected;

		AddChild(RightPanel = new MarginContainer());
	}

	private void EventList_OnEventSelected(EntityEventInfo? entityEvent) {
		foreach (var child in RightPanel.GetChildren()) {
			child.QueueFree();
		}
		if (entityEvent != null && _entity != null) RightPanel.AddChild(new EntityEventEditor(_resources, _entity, entityEvent));
	}

	public void Load(IBaseEntityInfo? entity) {
		_entity = entity;
		EventsList.Load(entity?.Events);
	}
}