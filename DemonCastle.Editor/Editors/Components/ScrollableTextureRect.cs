using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class ScrollableTextureRect : ScrollContainer {
	private Control TextureHolder { get; }
	private TextureRect TextureRect { get; }

	public Texture2D Texture {
		get => TextureRect.Texture;
		set => TextureRect.Texture = value;
	}

	public float Zoom { get; set; } = 1;

	public ScrollableTextureRect() {
		Name = nameof(ScrollableTextureRect);

		AddChild(TextureHolder = new Control());

		TextureHolder.AddChild(TextureRect = new TextureRect());
	}

	public override void _Process(double delta) {
		base._Process(delta);
		TextureRect.Scale = Vector2.One * Zoom;
		TextureHolder.CustomMinimumSize = TextureRect.Size * TextureRect.Scale;
	}
}