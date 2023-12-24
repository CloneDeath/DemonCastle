using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors;

public partial class AudioEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.SoundIcon;
	public override string TabText { get; }

	private readonly AudioStreamPlayer _player;

	public AudioEditor(FileNavigator file) {
		Name = nameof(ImageEditor);
		TabText = file.FileName;

		AddChild(_player = new AudioStreamPlayer {
			Stream = file.ToAudioStream()
		});
	}

	public override void _Ready() {
		base._Ready();


		_player.Play();
	}
}