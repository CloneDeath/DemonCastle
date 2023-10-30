using Godot;
using ControlViewFooter = DemonCastle.Editor.Editors.Components.ControlViewComponent.ControlViewFooter;
using ControlViewToolbar = DemonCastle.Editor.Editors.Components.ControlViewComponent.ControlViewToolbar;

namespace DemonCastle.Editor.Editors.Components;

public partial class TextureView : Container {
	protected ControlViewToolbar Toolbar { get; }
	protected ScrollableTextureRect TextureRect { get; }
	protected Grid TextureRect_Grid { get; }
	protected ControlViewFooter Footer { get; }

	public Texture2D? Texture {
		get => TextureRect.Texture;
		set => TextureRect.Texture = value;
	}

	public TextureView() {
		Name = nameof(TextureView);
		TextureFilter = TextureFilterEnum.Nearest;

		AddChild(Toolbar = new ControlViewToolbar());
		Toolbar.SetAnchorsPreset(LayoutPreset.TopWide);
		Toolbar.ZoomLevelChanged += Toolbar_OnZoomLevelChanged;

		AddChild(TextureRect = new ScrollableTextureRect {
			Name = nameof(TextureRect),
			OffsetTop = 30,
			OffsetBottom = -25
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

		AddChild(Footer = new ControlViewFooter {
			OffsetTop = -20
		});
		Footer.SetAnchorsPreset(LayoutPreset.BottomWide, true);
	}

	private void Toolbar_OnZoomLevelChanged(float zoom) {
		TextureRect.Zoom = zoom;
	}

	private void TextureRect_OnMouseEntered() {
		Footer.MousePositionVisible = true;
	}

	private void TextureRect_OnMouseExited() {
		Footer.MousePositionVisible = false;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (Texture == null) return;

		var size = Texture.GetSize();
		Footer.SetSizeText((Vector2I)size);
		var pixel = TextureRect.Inner.GetLocalMousePosition().Floor();
		Footer.SetMousePositionText((Vector2I)pixel);
		TextureRect_Grid.Visible = TextureRect.Zoom >= 4 && Toolbar.ShowGrid;
	}
}