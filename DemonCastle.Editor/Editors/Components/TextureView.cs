using DemonCastle.Editor.Editors.Components.TextureViewComponents;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class TextureView : Container {
	protected TextureViewToolbar Toolbar { get; }
	protected ScrollableTextureRect TextureRect { get; }
	protected Grid TextureRect_Grid { get; }
	protected HBoxContainer Footer { get; }
	protected Label Footer_SizeLabel { get; }
	protected Label Footer_MousePixel { get; }

	public Texture2D? Texture {
		get => TextureRect.Texture;
		set => TextureRect.Texture = value;
	}

	public TextureView() {
		Name = nameof(TextureView);
		TextureFilter = TextureFilterEnum.Nearest;

		AddChild(Toolbar = new TextureViewToolbar());
		Toolbar.SetAnchorsPreset(LayoutPreset.TopWide);
		Toolbar.ZoomLevelChanged += Toolbar_OnZoomLevelChanged;

		AddChild(TextureRect = new ScrollableTextureRect {
			Name = nameof(TextureRect),
			OffsetTop = 30,
			OffsetBottom = -35
		});
		TextureRect.Inner.MouseDefaultCursorShape = CursorShape.Cross;
		TextureRect.Inner.MouseEntered += TextureRect_OnMouseEntered;
		TextureRect.Inner.MouseExited += TextureRect_OnMouseExited;
		TextureRect.SetAnchorsPreset(LayoutPreset.FullRect, true);

		TextureRect.Inner.AddChild(TextureRect_Grid = new Grid {
			CellSize = Vector2I.One,
			Color = new Color(Colors.White, 0.5f),
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

	private void Toolbar_OnZoomLevelChanged(float zoom) {
		TextureRect.Zoom = zoom;
	}

	private void TextureRect_OnMouseEntered() {
		Footer_MousePixel.Visible = true;
	}

	private void TextureRect_OnMouseExited() {
		Footer_MousePixel.Visible = false;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (Texture == null) return;

		var size = Texture.GetSize();
		Footer_SizeLabel.Text = $"{size.X}x{size.Y}";
		var pixel = TextureRect.Inner.GetLocalMousePosition().Floor();
		Footer_MousePixel.Text = $"@{pixel.X}x{pixel.Y}";
		TextureRect_Grid.Visible = TextureRect.Zoom >= 4 && Toolbar.ShowGrid;
	}
}