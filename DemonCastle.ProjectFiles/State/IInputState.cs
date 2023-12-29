using DemonCastle.ProjectFiles.Files.SceneEvents;

namespace DemonCastle.ProjectFiles.State;

public interface IInputState {
	bool AnyInputIsInState(KeyState state);
	bool InputIsInState(PlayerAction action, KeyState state);
}