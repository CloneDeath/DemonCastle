using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.ProjectFiles.State;

public interface IGameState {
	IPlayerState Player { get; }
	void SetCharacter(CharacterInfo character);
	void SetLevel(LevelInfo level);
	void PushScene(SceneInfo scene);
	void PopScene(int number);
	void SetScene(SceneInfo scene);
	IInputState Input { get; }
	Texture2D LevelView { get; }
}

public interface IPlayerState {
	public int HP { get; }
	public int MP { get; }
	public int Lives { get; }
	public int Score { get; }
}