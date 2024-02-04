using System;
using Godot;

namespace DemonCastle.Editor;

public partial class EditorTopBar : HBoxContainer {
	private Button PlayButton { get; }
	private Button ProjectsButton { get; }

	public event Action? PlayPressed;
	public event Action? ProjectMenuPressed;

	public EditorTopBar() {
		Name = nameof(EditorTopBar);

		AddChild(PlayButton = new Button {
			Text = "Test Game"
		});
		PlayButton.Pressed += PlayButton_OnPressed;

		AddChild(ProjectsButton = new Button {
			Text = "Back to Project Selection"
		});
		ProjectsButton.Pressed += ProjectsButton_OnPressed;
	}

	private void PlayButton_OnPressed() => PlayPressed?.Invoke();
	private void ProjectsButton_OnPressed() => ProjectMenuPressed?.Invoke();
}