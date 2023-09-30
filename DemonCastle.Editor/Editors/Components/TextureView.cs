using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class TextureView : Container {
	protected HBoxContainer Controls { get; }
	protected ScrollableTextureRect TextureRect { get; }
	protected HBoxContainer Footer { get; }
	protected Label Footer_SizeLabel { get; }

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
			OffsetTop = 35,
			OffsetBottom = -35
		});
		TextureRect.SetAnchorsPreset(LayoutPreset.FullRect, true);

		AddChild(Footer = new HBoxContainer {
			CustomMinimumSize = new Vector2(100, 20),
			OffsetTop = -20
		});
		Footer.SetAnchorsPreset(LayoutPreset.BottomWide, true);
		Footer.AddChild(Footer_SizeLabel = new Label());
	}

	public override void _Process(double delta) {
		base._Process(delta);

		var size = Texture.GetSize();
		Footer_SizeLabel.Text = $"{size.X}x{size.Y}";
	}
}