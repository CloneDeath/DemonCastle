using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.Scenes;

public partial class ElementsView : Control {
	private readonly SceneInfo _scene;
	private readonly IGameState _gameState;
	private readonly ISceneState _sceneState;

	public ElementsView(SceneInfo scene, IGameState gameState, ISceneState sceneState) {
		_scene = scene;
		_gameState = gameState;
		_sceneState = sceneState;
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

		foreach (var element in _scene.Elements.Reverse()) {
			var elementView = ElementViewFactory.GetView(element, _gameState, _sceneState);
			AddChild(elementView);
		}
	}
}

public interface ISceneState {
	public bool IsActive { get; }
}