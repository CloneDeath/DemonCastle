using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;

namespace DemonCastle.ProjectFiles.State;

public interface IGameState {
	void SetCharacter(CharacterInfo character);
	void SetLevel(LevelInfo level);
	void PushScene(SceneInfo scene);
	void PopScene(int number);
	void SetScene(SceneInfo scene);
	IInputState Input { get; }
}