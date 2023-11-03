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
		MainControl.GetHScrollBar().MinValue = -2;
		MainControl.GetVScrollBar().MinValue = -2;
		MainControl.Inner.MouseDefaultCursorShape = CursorShape.Cross;
		MainControl.Inner.MouseEntered += MainControl_Inner_OnMouseEntered;
		MainControl.Inner.MouseExited += MainControl_Inner_OnMouseExited;
		MainControl.SetAnchorsPreset(LayoutPreset.FullRect, true);

		MainControl.Inner.AddChild(MainControl_Grid = new Grid {
			CellSize = Vector2I.One,
			Color = new Color(Colors.White, 0.1f),
			MouseFilter = MouseFilterEnum.Pass
		}, false, InternalMode.Front );
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

	public Color GridColor {
		get => MainControl_Grid.Color;
		set => MainControl_Grid.Color = value;
	}

	public bool GridVisible {
		get => Toolbar.ShowGrid;
		set => Toolbar.ShowGrid = value;
	}

	private void Toolbar_OnZoomLevelChanged(float zoom) {
		var previousZoom = MainControl.Zoom;
		MainControl.Zoom = zoom;
		MainControl.ScrollPosition = (Vector2I)((Vector2)MainControl.ScrollPosition * zoom / previousZoom);
	}

	private void MainControl_Inner_OnMouseEntered() {
		Footer.MousePositionVisible = true;
	}

	private void MainControl_Inner_OnMouseExited() {
		Footer.MousePositionVisible = false;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		var size = MainControl.Inner.Size / CellSize;
		Footer.SetSizeText((Vector2I)size);
		var pixel = MainControl.Inner.GetLocalMousePosition().Floor() / CellSize;
		Footer.SetMousePositionText((Vector2I)pixel);
		MainControl_Grid.Visible = CellSize * (int)MainControl.Zoom >= Vector2I.One * 4 && Toolbar.ShowGrid;
	}
}