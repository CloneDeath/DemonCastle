using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class Outline : Control {
	private Color _color = Colors.White;

	public Color Color {
		get => _color;
		set {
			_color = value;
			SetColor(value);
		}
	}

	private void SetColor(Color value) {
		foreach (var rect in _rects) {
			rect.Color = value;
		}
	}

	private ColorRect[] _rects = new ColorRect[4];

	public Outline() {
		// Top
		AddChild(_rects[0] = new ColorRect {
			Color = Color,
			OffsetTop = -1,
			MouseFilter = MouseFilterEnum.Pass
		});
		_rects[0].SetAnchorsPreset(LayoutPreset.TopWide, true);

		// Bottom
		AddChild(_rects[1] = new ColorRect {
			Color = Color,
			OffsetBottom = 1,
			MouseFilter = MouseFilterEnum.Pass
		});
		_rects[1].SetAnchorsPreset(LayoutPreset.BottomWide, true);

		// Left
		AddChild(_rects[2] = new ColorRect {
			Color = Color,
			OffsetLeft = -1,
			MouseFilter = MouseFilterEnum.Pass
		});
		_rects[2].SetAnchorsPreset(LayoutPreset.LeftWide, true);

		// Right
		AddChild(_rects[3] = new ColorRect {
			Color = Color,
			OffsetRight = 1,
			MouseFilter = MouseFilterEnum.Pass
		});
		_rects[3].SetAnchorsPreset(LayoutPreset.RightWide, true);
	}
}