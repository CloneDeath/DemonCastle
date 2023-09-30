using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class TextureView : ScrollContainer {
	protected TextureRect TextureRect { get; }

	public Texture2D Texture {
		get => TextureRect.Texture;
		set => TextureRect.Texture = value;
	}

	public TextureView() {
		Name = nameof(TextureView);

		AddChild(TextureRect = new TextureRect {
			Name = nameof(TextureRect)
		});
	}
}