using System.Collections.Specialized;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.View;

public partial class ElementsView : Control {
	private readonly SceneInfo _scene;

	public ElementsView(SceneInfo scene) {
		_scene = scene;
		Name = nameof(ElementsView);

		RefreshElements();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_scene.Elements.CollectionChanged += Elements_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_scene.Elements.CollectionChanged -= Elements_OnCollectionChanged;
	}

	private void Elements_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		RefreshElements();
	}

	private void RefreshElements() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		foreach (var element in _scene.Elements) {
			var elementView = EditorElementFactory.GetView(element);
			AddChild(elementView);
		}
	}
}