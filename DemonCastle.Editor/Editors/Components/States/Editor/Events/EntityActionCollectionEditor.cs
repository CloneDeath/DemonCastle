using DemonCastle.Editor.Editors.Components.Actions;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Events;

public partial class EntityActionCollectionEditor : ActionCollectionEditor<EntityActionInfo> {
	private readonly ProjectResources _resources;
	public IBaseEntityInfo? Entity { get; set; }

	public EntityActionCollectionEditor(ProjectResources resources) {
		_resources = resources;
		Name = nameof(EntityActionCollectionEditor);
	}

	protected override void AddAction(EntityActionInfo action) {
		if (Entity == null) return;
		if (_actionSet == null) return;

		var editor = new EntityActionEditor(_resources, Entity, action, _actionSet);
		Actions.AddChild(editor);
	}
}