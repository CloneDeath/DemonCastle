using System.Collections.Specialized;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Events;

public partial class SceneEventActionCollectionEditor : VBoxContainer {
	private readonly IFileInfo _file;
	private readonly SceneEventActionInfoCollection _then;
	public VBoxContainer Actions { get; }
	public Button AddActionButton { get; }

	public SceneEventActionCollectionEditor(IFileInfo file, SceneEventActionInfoCollection then) {
		_file = file;
		_then = then;
		Name = nameof(SceneEventActionCollectionEditor);

		AddChild(new Label { Text = "Then" });
		AddChild(Actions = new VBoxContainer());
		AddChild(AddActionButton = new Button { Text = "Add Action" });
		AddActionButton.Pressed += () => {
			then.AppendNew();
		};

		Reload();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_then.CollectionChanged += Then_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_then.CollectionChanged -= Then_OnCollectionChanged;
	}

	private void Then_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		Reload();
	}

	private void Reload() {
		foreach (var child in Actions.GetChildren()) {
			child.QueueFree();
		}

		foreach (var action in _then) {
			SceneEventActionEditor editor;
			Actions.AddChild(editor = new SceneEventActionEditor(_file, action));
			editor.Deleted += () => {
				editor.QueueFree();
				_then.Remove(action);
			};
		}
	}
}