using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class TextureView : Container {
	protected HBoxContainer Controls { get; }
	protected Button Controls_MagPlus { get; }
	protected Button Controls_MagMinus { get; }
	protected ScrollableTextureRect TextureRect { get; }
	protected HBoxContainer Footer { get; }
	protected Label Footer_SizeLabel { get; }
	protected Label Footer_MousePixel { get; }

	public Texture2D Texture {
		get => TextureRect.Texture;
		set => TextureRect.Texture = value;
	}

	public TextureView() {
		Name = nameof(TextureView);
		TextureFilter = TextureFilterEnum.Nearest;


		AddChild(Controls = new HBoxContainer {
			CustomMinimumSize = new Vector2(100, 30)
		});
		Controls.AddChild(Controls_MagPlus = new Button { Icon = IconTextures.MagnifyPlusIcon });
		Controls_MagPlus.Pressed += Controls_MagPlus_OnPressed;
		Controls.AddChild(Controls_MagMinus = new Button { Icon = IconTextures.MagnifyMinusIcon });
		Controls_MagMinus.Pressed += Controls_MagMinus_OnPressed;
		Controls.SetAnchorsPreset(LayoutPreset.TopWide);

		AddChild(TextureRect = new ScrollableTextureRect {
			Name = nameof(TextureRect),
			OffsetTop = 35,
			OffsetBottom = -35
		});
		TextureRect.InnerTexture.MouseEntered += TextureRect_OnMouseEntered;
		TextureRect.InnerTexture.MouseExited += TextureRect_OnMouseExited;
		TextureRect.SetAnchorsPreset(LayoutPreset.FullRect, true);

		AddChild(Footer = new HBoxContainer {
			CustomMinimumSize = new Vector2(100, 20),
			OffsetTop = -20
		});
		Footer.SetAnchorsPreset(LayoutPreset.BottomWide, true);
		Footer.AddChild(Footer_SizeLabel = new Label());
		Footer.AddChild(Footer_MousePixel = new Label { Visible = false });
	}

	private void TextureRect_OnMouseEntered() {
		Footer_MousePixel.Visible = true;
	}

	private void TextureRect_OnMouseExited() {
		Footer_MousePixel.Visible = false;
	}

	private void Controls_MagPlus_OnPressed() {
		TextureRect.Zoom *= 2;
	}

	private void Controls_MagMinus_OnPressed() {
		TextureRect.Zoom /= 2;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		var size = Texture.GetSize();
		Footer_SizeLabel.Text = $"{size.X}x{size.Y}";
		var pixel = TextureRect.InnerTexture.GetLocalMousePosition().Floor();
		Footer_MousePixel.Text = $"@{pixel.X}x{pixel.Y}";
	}
}