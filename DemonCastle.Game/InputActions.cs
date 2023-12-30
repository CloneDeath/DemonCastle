using System.Linq;
using Godot;

namespace DemonCastle.Game;

public static class InputActions {
	public const string PlayerMoveLeft = nameof(PlayerMoveLeft);
	public const string PlayerMoveRight = nameof(PlayerMoveRight);
	public const string PlayerMoveUp = nameof(PlayerMoveUp);
	public const string PlayerMoveDown = nameof(PlayerMoveDown);
	public const string PlayerJump = nameof(PlayerJump);
	public const string PlayerAttack = nameof(PlayerAttack);

	public static readonly string[] PlayerActions = { PlayerMoveLeft, PlayerMoveRight, PlayerMoveUp, PlayerMoveDown, PlayerJump, PlayerAttack };

	public const string EditorSubmit = nameof(EditorSubmit);
	public const string EditorClick = nameof(EditorClick);
	public const string EditorRightClick = nameof(EditorRightClick);
	public const string EditorRename = nameof(EditorRename);
	public const string EditorSave = nameof(EditorSave);

	public static void RegisterActions() {
		RegisterAction(PlayerMoveLeft, Key.Left, Key.A);
		RegisterAction(PlayerMoveRight, Key.Right, Key.D);
		RegisterAction(PlayerJump, Key.Space, Key.Z);
		RegisterAction(PlayerAttack, Key.F, Key.X);
		RegisterAction(EditorSubmit, Key.Enter, Key.KpEnter);
		RegisterAction(EditorClick, MouseButton.Left);
		RegisterAction(EditorRightClick, MouseButton.Right);
		RegisterAction(EditorRename, Key.F2);
		RegisterAction(PlayerMoveUp, Key.Up, Key.W);
		RegisterAction(PlayerMoveDown, Key.Down, Key.S);
		RegisterAction(EditorSave, new InputEventKey {
			Keycode = Key.S,
			CtrlPressed = true
		});
	}

	private static void RegisterAction(string actionName, params Key[] keyList) {
		RegisterAction(actionName, keyList.Select(key => (InputEvent)new InputEventKey {
			Keycode = key
		}).ToArray());
	}

	// ReSharper disable once UnusedMember.Local
	private static void RegisterAction(string actionName, params MouseButton[] mouseButtons) {
		RegisterAction(actionName, mouseButtons.Select(button => (InputEvent)new InputEventMouseButton {
			ButtonIndex = button
		}).ToArray());
	}

	private static void RegisterAction(string actionName, params InputEvent[] events) {
		if (InputMap.HasAction(actionName)) return;
		InputMap.AddAction(actionName);
		foreach (var inputEvent in events) {
			InputMap.ActionAddEvent(actionName, inputEvent);
		}
	}
}