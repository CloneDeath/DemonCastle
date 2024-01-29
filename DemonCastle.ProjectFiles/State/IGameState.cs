using System;
using DemonCastle.ProjectFiles.Locations;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.ProjectFiles.State;

public interface IGameState {
	IPlayerState Player { get; }
	ICurrentArea? CurrentArea { get; }

	IVariables Variables { get; }

	void SetCharacter(CharacterInfo character);
	void SetLevel(LevelInfo level);
	void PushScene(SceneInfo scene);
	void PopScene(int number);
	void SetScene(SceneInfo scene);
	IInputState Input { get; }
	Texture2D LevelView { get; }
	void SpawnItem(Guid itemId, Vector2 position);
	void Restart();
	void Quit();
}

public interface ICurrentArea {
	AreaPosition Position { get; }
}