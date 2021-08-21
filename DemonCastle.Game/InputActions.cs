using System.Linq;
using Godot;

namespace DemonCastle.Game {
	public static class InputActions {
		public const string PlayerMoveLeft = nameof(PlayerMoveLeft);
		public const string PlayerMoveRight = nameof(PlayerMoveRight);
		public const string PlayerJump = nameof(PlayerJump);
		
		public static void RegisterActions() {
			RegisterAction(PlayerMoveLeft, KeyList.Left, KeyList.A);
			RegisterAction(PlayerMoveRight, KeyList.Right, KeyList.D);
			RegisterAction(PlayerJump, KeyList.Up, KeyList.W);
		}

		private static void RegisterAction(string actionName, params KeyList[] keyList) {
			RegisterAction(actionName, keyList.Select(key => (InputEvent)new InputEventKey {
				Scancode = (uint) key
			}).ToArray());
		}
		
		// ReSharper disable once UnusedMember.Local
		private static void RegisterAction(string actionName, params ButtonList[] mouseButtons) {
			RegisterAction(actionName, mouseButtons.Select(button => (InputEvent)new InputEventMouseButton {
				ButtonIndex = (int) button
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