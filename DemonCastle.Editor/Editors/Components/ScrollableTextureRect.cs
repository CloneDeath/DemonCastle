using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class ScrollableTextureRect : ScrollContainer {
	private TextureRect TextureRect { get; }

	public Texture2D Texture {
		get => TextureRect.Texture;
		set => TextureRect.Texture = value;
	}

	public ScrollableTextureRect() {
		Name = nameof(ScrollableTextureRect);

		AddChild(TextureRect = new TextureRect());
	}
}