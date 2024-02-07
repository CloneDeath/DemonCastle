using System.Linq;
using Godot;

namespace DemonCastle.Game;

public static class InputActions {
	public const string MoveLeft = nameof(MoveLeft);
	public const string MoveRight = nameof(MoveRight);
	public const string MoveUp = nameof(MoveUp);
	public const string MoveDown = nameof(MoveDown);
	public const string Jump = nameof(Jump);
	public const string Attack = nameof(Attack);

	public const string Accept = nameof(Accept);
	public const string Cancel = nameof(Cancel);

	public static readonly string[] Actions = {
		MoveLeft, MoveRight, MoveUp, MoveDown, Jump, Attack,
		Accept, Cancel
	};

	public const string EditorSubmit = nameof(EditorSubmit);
	public const string EditorClick = nameof(EditorClick);
	public const string EditorRightClick = nameof(EditorRightClick);
	public const string EditorRename = nameof(EditorRename);
	public const string EditorSave = nameof(EditorSave);
	public const string EditorClose = nameof(EditorClose);

	public static void RegisterActions() {
		RegisterAction(MoveLeft, Key.Left, Key.A);
		RegisterAction(MoveRight, Key.Right, Key.D);
		RegisterAction(MoveUp, Key.Up, Key.W);
		RegisterAction(MoveDown, Key.Down, Key.S);
		RegisterAction(Jump, Key.Space, Key.Z);
		RegisterAction(Attack, Key.F, Key.X);
		RegisterAction(Accept, Key.F, Key.X, Key.Enter, Key.KpEnter, Key.Space);
		RegisterAction(Cancel, Key.Q, Key.Z, Key.Backspace, Key.Escape);

		RegisterAction(EditorSubmit, Key.Enter, Key.KpEnter);
		RegisterAction(EditorClick, MouseButton.Left);
		RegisterAction(EditorRightClick, MouseButton.Right);
		RegisterAction(EditorRename, Key.F2);
		RegisterAction(EditorSave, new InputEventKey {
			Keycode = Key.S,
			CtrlPressed = true
		});
		RegisterAction(EditorClose, new InputEventKey {
			Keycode = Key.W,
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