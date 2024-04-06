using System.Collections.Specialized;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Actions;

public abstract partial class ActionCollectionEditor<TInfo> : VBoxContainer {
	protected IEnumerableInfo<TInfo>? _actionSet;

	public VBoxContainer Actions { get; }
	public Button AddActionButton { get; }

	protected ActionCollectionEditor() {
		Name = nameof(ActionCollectionEditor<TInfo>);
		AddChild(Actions = new VBoxContainer());
		AddChild(AddActionButton = new Button { Text = "Add Action" });
		AddActionButton.Pressed += AddActionButton_OnPressed;
	}

	private void AddActionButton_OnPressed() => _actionSet?.AppendNew();

	public override void _ExitTree() {
		base._ExitTree();
		if (_actionSet != null) _actionSet.CollectionChanged -= ActionSet_OnCollectionChanged;
	}

	public void Load(IEnumerableInfo<TInfo>? actionSet) {
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

	protected abstract void AddAction(TInfo action);
}