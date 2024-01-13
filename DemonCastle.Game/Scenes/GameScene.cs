using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.State;
using Godot;
using SceneState = DemonCastle.ProjectFiles.State.SceneState;

namespace DemonCastle.Game.Scenes;

public partial class GameScene : Control, ISceneState {
	private SceneInfo? _scene;
	private readonly IGameState _gameState;

	public bool IsActive { get; set; }

	public GameScene(IGameState gameState) {
		_gameState = gameState;
		Name = nameof(GameScene);
	}

	public Vector2 SceneSize => _scene?.Size ?? Vector2.Zero;

	public void Load(SceneInfo scene) {
		IsActive = true;
		_scene?.TriggerEvents(_gameState, new SceneState {
			OnExit = true
		});
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		_scene = scene;
		AddChild(new ColorRect {
			Size = scene.Size,
			Color = scene.BackgroundColor
		});
		AddChild(new ElementsView(scene, _gameState, this));
		_scene.TriggerEvents(_gameState, new SceneState {
			OnEnter = true
		});
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (!IsActive) return;
		_scene?.TriggerEvents(_gameState, new SceneState());
	}

	public void Unload() {
		_scene?.TriggerEvents(_gameState, new SceneState {
			OnExit = true
		});
		_scene = null;
	}
}