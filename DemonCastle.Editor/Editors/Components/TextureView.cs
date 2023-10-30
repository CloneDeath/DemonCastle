using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class TextureView : ControlView<TextureRect> {
	public Texture2D? Texture {
		get => MainControl.Inner.Texture;
		set => MainControl.Inner.Texture = value;
	}

	public TextureView() {
		Name = nameof(TextureView);
	}
}