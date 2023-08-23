using System.Linq;
using Godot;

namespace DemonCastle.Game {
	public static class InputActions {
		public const string PlayerMoveLeft = nameof(PlayerMoveLeft);
		public const string PlayerMoveRight = nameof(PlayerMoveRight);
		public const string PlayerJump = nameof(PlayerJump);

		public const string EditorSubmit = nameof(EditorSubmit);
		
		public static void RegisterActions() {
			RegisterAction(PlayerMoveLeft, Key.Left, Key.A);
			RegisterAction(PlayerMoveRight, Key.Right, Key.D);
			RegisterAction(PlayerJump, Key.Up, Key.W);
			RegisterAction(EditorSubmit, Key.Enter, Key.KpEnter);
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
}