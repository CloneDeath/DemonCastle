using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.Scenes;

public partial class SceneStack : Control {
	private readonly IGameState _gameState;
	private readonly Stack<GameScene> _stack = new();

	public Vector2 SceneSize => _stack.TryPeek(out var scene) ? scene.SceneSize : Vector2.Zero;

	public SceneStack(IGameState gameState) {
		_gameState = gameState;
	}

	public void Set(SceneInfo scene) {
		var gameScene = new GameScene(_gameState);
		_stack.Clear();
		_stack.Push(gameScene);
		gameScene.Load(scene);
		AddChild(gameScene);
	}

	public void Push(SceneInfo scene) {
		var gameScene = new GameScene(_gameState);
		_stack.Push(gameScene);
		gameScene.Load(scene);
		AddChild(gameScene);
	}

	public void Pop(int number) {
		for (var i = 0; i < number; i++) {
			var gameScene = _stack.Pop();
			gameScene.Unload();
			gameScene.QueueFree();
		}
	}
}