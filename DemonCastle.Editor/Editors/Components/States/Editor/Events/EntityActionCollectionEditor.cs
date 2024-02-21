using System.Collections.Specialized;
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
		if (_actionSet != null) _actionSet.CollectionChanged -= ActionSet_OnCollectionChanged;
		_actionSet = actionSet;
		if (_actionSet != null) _actionSet.CollectionChanged += ActionSet_OnCollectionChanged;

		Reload();
	}

	private void ActionSet_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		Reload();
	}

	private void Reload() {
		Clear();
		if (_actionSet == null) return;

		foreach (var action in _actionSet) {
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
		if (_actionSet == null) return;

		var editor = new EntityActionEditor(_resources, Entity, action, _actionSet);
		Actions.AddChild(editor);
		editor.SetAnchorsPreset(LayoutPreset.FullRect);
	}
}