using System;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.GameStates;

public class GameState : IGameState {
	private readonly GameRunner _runner;

	public GameState(GameRunner runner) {
		_runner = runner;
	}

	public IPlayerState Player => _runner.GamePlayer.PlayerState;
	public ICurrentArea? CurrentArea => _runner.CurrentArea != null ? new CurrentAreaState(_runner.CurrentArea) : null;

	public void SetCharacter(CharacterInfo character) => _runner.SetCharacter(character);
	public void SetLevel(LevelInfo level) => _runner.SetLevel(level);

	public void SetScene(SceneInfo scene) => _runner.SceneStack.Set(scene);
	public void PushScene(SceneInfo scene) => _runner.SceneStack.Push(scene);
	public void PopScene(int number) => _runner.SceneStack.Pop(number);

	public IInputState Input => new InputState();
	public Texture2D LevelView => _runner.LevelViewport.GetTexture();
	public void SpawnItem(Guid itemId, Vector2 position) => _runner.SpawnItem(itemId, position);
}