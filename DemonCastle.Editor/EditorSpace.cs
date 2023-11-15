using DemonCastle.Game;
using DemonCastle.Game.SetupScreen;
using Godot;

namespace DemonCastle.Editor;

public partial class EditorSpace : CanvasLayer {
	private void PlayPressed() {
		foreach (var child in PlayWindow.GetChildren()) {
			child.QueueFree();
		}

		var gameSetup = new GameSetup(Project);
		gameSetup.GameStart += (level, character) => {
			gameSetup.QueueFree();
			var runner = new GameRunner(level, character);
			PlayWindow.AddChild(runner);
			runner.SetAnchorsPreset(Control.LayoutPreset.FullRect);
		};
		PlayWindow.AddChild(gameSetup);
		PlayWindow.PopupCentered();
	}
}