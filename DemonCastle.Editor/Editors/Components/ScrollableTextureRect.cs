using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class ScrollableTextureRect : ScrollableWrapper<TextureRect> {
	public Texture2D? Texture {
		get => Inner.Texture;
		set => Inner.Texture = value;
	}

	public ScrollableTextureRect() {
		Name = nameof(ScrollableTextureRect);
	}
}