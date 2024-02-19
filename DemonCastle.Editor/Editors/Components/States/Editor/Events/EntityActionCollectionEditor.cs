using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Events;

public partial class EntityActionCollectionEditor : VBoxContainer {
	private readonly ProjectResources _resources;
	private EntityActionInfoCollection? _actionSet;
	public IBaseEntityInfo? Entity { get; set; }

	public VBoxContainer Actions { get; }
	public Button AddActionButton { get; }

	public EntityActionCollectionEditor(ProjectResources resources) {
		_resources = resources;
		Name = nameof(EntityActionCollectionEditor);

		AddChild(Actions = new VBoxContainer());
		AddChild(AddActionButton = new Button { Text = "Add Action" });
		AddActionButton.Pressed += AddActionButton_OnPressed;
	}

	private void AddActionButton_OnPressed() => _actionSet?.AppendNew();

	public void Load(EntityActionInfoCollection? actionSet) {
		_actionSet = actionSet;
		Clear();
		if (actionSet == null) return;

		foreach (var action in actionSet) {
			AddAction(action);
		}
	}

	public void Clear() {
		foreach (var child in Actions.GetChildren()) {
			child.QueueFree();
		}
	}

	private void AddAction(EntityActionInfo action) {
		if (Entity == null) return;

		var editor = new EntityActionEditor(_resources, Entity, action);
		Actions.AddChild(editor);
		editor.SetAnchorsPreset(LayoutPreset.FullRect);
	}
}