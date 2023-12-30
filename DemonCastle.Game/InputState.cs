using System;
using System.Linq;
using DemonCastle.ProjectFiles.Files.SceneEvents;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public class InputState : IInputState {
	public bool AnyInputIsInState(KeyState state) {
		var actions = Enum.GetValues<PlayerAction>();
		return actions.Any(playerAction => InputIsInState(playerAction, state));
	}

	public bool InputIsInState(PlayerAction action, KeyState state) {
		var actionName = GetActionName(action);
		return state switch {
			KeyState.Pressed => Input.IsActionJustPressed(actionName),
			KeyState.Released => Input.IsActionJustReleased(actionName),
			KeyState.Down => Input.IsActionPressed(actionName),
			KeyState.Up => !Input.IsActionPressed(actionName),
			_ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
		};
	}

	private static string GetActionName(PlayerAction action) => action switch {
		PlayerAction.Up => InputActions.PlayerMoveUp,
		PlayerAction.Down => InputActions.PlayerMoveDown,
		PlayerAction.Left => InputActions.PlayerMoveLeft,
		PlayerAction.Right => InputActions.PlayerMoveRight,
		PlayerAction.Jump => InputActions.PlayerJump,
		PlayerAction.Attack => InputActions.PlayerAttack,
		_ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
	};
}