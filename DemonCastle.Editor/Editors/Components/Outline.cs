using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class Outline : Control {
	private int _thickness = 1;
	private Color _color = Colors.White;

	public Color Color {
		get => _color;
		set {
			_color = value;
			SetColor();
		}
	}

	public int Thickness {
		get => _thickness;
		set {
			_thickness = value;
			SetThickness();
		}
	}

	private void SetColor() {
		foreach (var rect in _rects) {
			rect.Color = _color;
		}
	}

	private void SetThickness() {
		_rects[0].OffsetTop = -_thickness;
		_rects[1].OffsetBottom = _thickness;
		_rects[2].OffsetLeft = -_thickness;
		_rects[3].OffsetRight = _thickness;
	}

	private ColorRect[] _rects = new ColorRect[4];

	public Outline() {
		// Top
		AddChild(_rects[0] = new ColorRect { MouseFilter = MouseFilterEnum.Pass });
		_rects[0].SetAnchorsPreset(LayoutPreset.TopWide);

		// Bottom
		AddChild(_rects[1] = new ColorRect { MouseFilter = MouseFilterEnum.Pass });
		_rects[1].SetAnchorsPreset(LayoutPreset.BottomWide, true);

		// Left
		AddChild(_rects[2] = new ColorRect { MouseFilter = MouseFilterEnum.Pass });
		_rects[2].SetAnchorsPreset(LayoutPreset.LeftWide, true);

		// Right
		AddChild(_rects[3] = new ColorRect { MouseFilter = MouseFilterEnum.Pass });
		_rects[3].SetAnchorsPreset(LayoutPreset.RightWide, true);

		SetColor();
		SetThickness();
	}
}