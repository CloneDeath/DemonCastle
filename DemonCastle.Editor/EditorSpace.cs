using DemonCastle.Game;
using DemonCastle.Game.SetupScreen;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor;

public partial class EditorSpace : CanvasLayer {
	private EditorTopBar TopBar;
	protected ProjectInfo Project { get; }
	protected Window PlayWindow;

	public EditorSpace(ProjectInfo project) {
		Name = nameof(EditorSpace);

		Project = project;
		AddChild(PlayWindow = new Window {
			Name = nameof(PlayWindow),
			Title = Project.Name,
			Visible = false,
			MinSize = new Vector2I(800, 600),
			Exclusive = true
		});
		PlayWindow.CloseRequested += PlayWindow.Hide;
		AddChild(TopBar = new EditorTopBar {
			AnchorRight = 1,
			OffsetRight = 0,
			OffsetTop = 5,
			OffsetLeft = 5
		});
		TopBar.PlayPressed += PlayPressed;
		AddChild(new EditorWorkspace(project) {
			AnchorRight = 1,
			AnchorBottom = 1,
			OffsetRight = -5,
			OffsetLeft = 5,
			OffsetBottom = -5,
			OffsetTop = 40
		});
	}

	private void PlayPressed() {
		foreach (var child in PlayWindow.GetChildren()) {
			child.QueueFree();
		}

		var gameSetup = new GameSetup(Project);
		gameSetup.GameStart += (level, character, debug) => {
			gameSetup.QueueFree();
			var runner = new GameRunner(Project, debug);
			PlayWindow.AddChild(runner);
			runner.SetAnchorsPreset(Control.LayoutPreset.FullRect);
		};
		PlayWindow.AddChild(gameSetup);
		PlayWindow.PopupCentered();
	}
}