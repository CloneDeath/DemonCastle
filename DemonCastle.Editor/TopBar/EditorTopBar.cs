using System;
using Godot;

namespace DemonCastle.Editor.TopBar; 

public partial class EditorTopBar : HBoxContainer {
	private Button PlayButton { get; }

	public Action? PlayPressed;
	
	public EditorTopBar() {
		AddChild(PlayButton = new Button {
			Text = "Play"
		});
		PlayButton.Pressed += PlayButtonOnPressed;
	}

	private void PlayButtonOnPressed() {
		PlayPressed?.Invoke();
	}
}