using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class TextureView : Container {
	protected HBoxContainer Controls { get; }
	protected Button Controls_Grid { get; }
	protected Button Controls_MagPlus { get; }
	protected Button Controls_MagMinus { get; }
	protected Button Controls_OneToOne { get; }
	protected ScrollableTextureRect TextureRect { get; }
	protected Grid TextureRect_Grid { get; }
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
			CustomMinimumSize = new Vector2(100, 10)
		});
		Controls.AddChild(Controls_Grid = new Button { Icon = IconTextures.GridIcon });
		Controls.AddChild(Controls_MagPlus = new Button { Icon = IconTextures.MagnifyPlusIcon });
		Controls_MagPlus.Pressed += Controls_MagPlus_OnPressed;
		Controls.AddChild(Controls_MagMinus = new Button { Icon = IconTextures.MagnifyMinusIcon });
		Controls_MagMinus.Pressed += Controls_MagMinus_OnPressed;
		Controls.AddChild(Controls_OneToOne = new Button { Icon = IconTextures.OneToOneIcon });
		Controls_OneToOne.Pressed += Controls_OneToOne_OnPressed;
		Controls.SetAnchorsPreset(LayoutPreset.TopWide);

		AddChild(TextureRect = new ScrollableTextureRect {
			Name = nameof(TextureRect),
			OffsetTop = 30,
			OffsetBottom = -35
		});
		TextureRect.InnerTexture.MouseEntered += TextureRect_OnMouseEntered;
		TextureRect.InnerTexture.MouseExited += TextureRect_OnMouseExited;
		TextureRect.SetAnchorsPreset(LayoutPreset.FullRect, true);

		TextureRect.InnerTexture.AddChild(TextureRect_Grid = new Grid {
			CellSize = Vector2I.One,
			MouseFilter = MouseFilterEnum.Pass
		});
		TextureRect_Grid.SetAnchorsPreset(LayoutPreset.FullRect);

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

	private void Controls_OneToOne_OnPressed() {
		TextureRect.Zoom = 1;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		var size = Texture.GetSize();
		Footer_SizeLabel.Text = $"{size.X}x{size.Y}";
		var pixel = TextureRect.InnerTexture.GetLocalMousePosition().Floor();
		Footer_MousePixel.Text = $"@{pixel.X}x{pixel.Y}";
	}
}