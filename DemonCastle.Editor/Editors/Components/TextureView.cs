using DemonCastle.Editor.Extensions;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class TextureView : Container {
	protected HBoxContainer Controls { get; }
	protected ScrollableTextureRect TextureRect { get; }

	public Texture2D Texture {
		get => TextureRect.Texture;
		set => TextureRect.Texture = value;
	}

	public TextureView() {
		Name = nameof(TextureView);

		AddChild(Controls = new HBoxContainer {
			CustomMinimumSize = new Vector2(100, 30)
		});
		Controls.AddChild(new Button { Text = "+"});
		Controls.AddChild(new Button { Text = "-"});
		Controls.SetAnchorsPreset(LayoutPreset.TopWide);

		AddChild(TextureRect = new ScrollableTextureRect {
			Name = nameof(TextureRect),
			OffsetTop = 35
		});
		TextureRect.SetAnchorsPreset(LayoutPreset.FullRect, true);
	}
}