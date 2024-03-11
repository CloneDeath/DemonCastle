using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Components.States.Editor.Events;
using DemonCastle.Editor.Editors.Scene.Events;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Events;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Components.BaseEntity.Events;

public partial class EntityEventEditor : PropertyCollection {
	protected EntityActionCollectionEditor Then { get; }
	public EntityEventEditor(ProjectResources resources, IBaseEntityInfo entity, EntityEventInfo entityEvent) {
		Name = nameof(SceneEventEditor);

		AddString("Name", entityEvent, e => e.Name);
		AddChild(new EntityEventConditionEditor(entityEvent.When));
		AddChild(new Label { Text = "Then" });
		AddChild(Then = new EntityActionCollectionEditor(resources));
		Then.Entity = entity;
		Then.Load(entityEvent.Then);
	}
}