using Godot;

namespace DemonCastle.Editor.Editors;

public partial class AudioEditor : BaseEditor {
	private readonly AudioStreamPlayer _player;
	private readonly Button _playButton;

	public AudioEditor(AudioStream audioStream) {
		Name = nameof(ImageEditor);

		AddChild(_player = new AudioStreamPlayer {
			Stream = audioStream
		});
		AddChild(_playButton = new Button {
			Text = "Play"
		});
		_playButton.Pressed += PlayButton_OnPressed;
	}

	private void PlayButton_OnPressed() {
		_player.Play();
	}
}