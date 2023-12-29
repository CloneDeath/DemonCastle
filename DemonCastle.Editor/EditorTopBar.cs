using System;
using Godot;

namespace DemonCastle.Editor;

public partial class EditorTopBar : HBoxContainer {
	private Button PlayButton { get; }

	public Action? PlayPressed;

	public EditorTopBar() {
		Name = nameof(EditorTopBar);

		AddChild(PlayButton = new Button {
			Text = "Play Level"
		});
		PlayButton.Pressed += PlayButtonOnPressed;
	}

	private void PlayButtonOnPressed() {
		PlayPressed?.Invoke();
	}
}