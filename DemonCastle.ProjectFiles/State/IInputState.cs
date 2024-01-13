using DemonCastle.Files.Conditions;

namespace DemonCastle.ProjectFiles.State;

public interface IInputState {
	bool AnyInputIsInState(KeyState state);
	bool InputIsInState(PlayerAction action, KeyState state);
}