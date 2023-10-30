using Godot;

namespace DemonCastle.Editor.Editors.Components.ControlViewComponent;

public partial class ControlView<T> : Container where T : Control, new()  {
	protected ControlViewToolbar Toolbar { get; }
	protected ScrollableWrapper<T> MainControl { get; }
	protected Grid MainControl_Grid { get; }
	protected ControlViewFooter Footer { get; }

	public ControlView() {
		Name = nameof(ControlView<T>);
		TextureFilter = TextureFilterEnum.Nearest;

		AddChild(Toolbar = new ControlViewToolbar());
		Toolbar.SetAnchorsPreset(LayoutPreset.TopWide);
		Toolbar.ZoomLevelChanged += Toolbar_OnZoomLevelChanged;

		AddChild(MainControl = new ScrollableWrapper<T> {
			OffsetTop = 30,
			OffsetBottom = -25
		});
		MainControl.Inner.MouseDefaultCursorShape = CursorShape.Cross;
		MainControl.Inner.MouseEntered += TextureRect_OnMouseEntered;
		MainControl.Inner.MouseExited += TextureRect_OnMouseExited;
		MainControl.SetAnchorsPreset(LayoutPreset.FullRect, true);

		MainControl.Inner.AddChild(MainControl_Grid = new Grid {
			CellSize = Vector2I.One,
			Color = new Color(Colors.White, 0.5f),
			MouseFilter = MouseFilterEnum.Pass
		});
		MainControl_Grid.SetAnchorsPreset(LayoutPreset.FullRect);

		AddChild(Footer = new ControlViewFooter {
			OffsetTop = -20
		});
		Footer.SetAnchorsPreset(LayoutPreset.BottomWide, true);
	}

	public Vector2I CellSize {
		get => MainControl_Grid.CellSize;
		set => MainControl_Grid.CellSize = value;
	}

	private void Toolbar_OnZoomLevelChanged(float zoom) {
		MainControl.Zoom = zoom;
	}

	private void TextureRect_OnMouseEntered() {
		Footer.MousePositionVisible = true;
	}

	private void TextureRect_OnMouseExited() {
		Footer.MousePositionVisible = false;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		
		var size = MainControl.Size;
		Footer.SetSizeText((Vector2I)size);
		var pixel = MainControl.Inner.GetLocalMousePosition().Floor();
		Footer.SetMousePositionText((Vector2I)pixel);
		MainControl_Grid.Visible = MainControl.Zoom >= 4 && Toolbar.ShowGrid;
	}
}