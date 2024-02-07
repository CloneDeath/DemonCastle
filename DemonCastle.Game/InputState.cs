using System;
using System.Linq;
using DemonCastle.Files.Conditions;
using DemonCastle.ProjectFiles.Exceptions;
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
			_ => throw new InvalidEnumValueException<KeyState>(state)
		};
	}

	private static string GetActionName(PlayerAction action) => action switch {
		PlayerAction.Up => InputActions.MoveUp,
		PlayerAction.Down => InputActions.MoveDown,
		PlayerAction.Left => InputActions.MoveLeft,
		PlayerAction.Right => InputActions.MoveRight,
		PlayerAction.Jump => InputActions.Jump,
		PlayerAction.Attack => InputActions.Attack,
		PlayerAction.Accept => InputActions.Accept,
		PlayerAction.Cancel => InputActions.Cancel,
		_ => throw new InvalidEnumValueException<PlayerAction>(action)
	};
}