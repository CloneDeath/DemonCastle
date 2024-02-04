using DemonCastle.Editor.Icons;
using DemonCastle.Navigation;
using Godot;

namespace DemonCastle.Editor.Editors;

public partial class AudioEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.File.SoundIcon;
	public override string TabText { get; }

	private readonly AudioStreamPlayer _player;
	private readonly Button _playButton;

	public AudioEditor(FileNavigator file) {
		Name = nameof(ImageEditor);
		TabText = file.FileName;

		AddChild(_player = new AudioStreamPlayer {
			Stream = file.ToAudioStream()
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