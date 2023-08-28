using DemonCastle.Game;
using DemonCastle.Game.SetupScreen;
using Godot;

namespace DemonCastle.Editor {
	public partial class EditorSpace : CanvasLayer {
		private void PlayPressed() {
			foreach (var child in PlayWindow.GetChildren()) {
				child.QueueFree();
			}

			var gameSetup = new GameSetup(Project);
			gameSetup.GameStart += (level, character) => {
				gameSetup.QueueFree();
				PlayWindow.AddChild(new GameRunner(level, character));
			};
			PlayWindow.AddChild(gameSetup);
			PlayWindow.PopupCentered();
		}
	}
}