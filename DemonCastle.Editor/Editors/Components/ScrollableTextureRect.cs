using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class ScrollableTextureRect : ScrollContainer {
	private Control TextureHolder { get; }
	public TextureRect InnerTexture { get; }

	public Texture2D? Texture {
		get => InnerTexture.Texture;
		set => InnerTexture.Texture = value;
	}

	public float Zoom { get; set; } = 1;

	public ScrollableTextureRect() {
		Name = nameof(ScrollableTextureRect);

		AddChild(TextureHolder = new Control());

		TextureHolder.AddChild(InnerTexture = new TextureRect());
	}

	public override void _Process(double delta) {
		base._Process(delta);
		InnerTexture.Scale = Vector2.One * Zoom;
		TextureHolder.CustomMinimumSize = InnerTexture.Size * InnerTexture.Scale;
	}
}