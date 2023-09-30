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
			OffsetTop = -1
		});
		_rects[0].SetAnchorsPreset(LayoutPreset.TopWide, true);

		// Bottom
		AddChild(_rects[1] = new ColorRect {
			Color = Color,
			AnchorLeft = 0,
			AnchorRight = 1,
			AnchorTop = 1,
			AnchorBottom = 1,
			OffsetBottom = 1
		});

		// Left
		AddChild(_rects[2] = new ColorRect {
			Color = Color,
			AnchorLeft = 0,
			AnchorRight = 0,
			AnchorTop = 0,
			AnchorBottom = 1,
			OffsetLeft = -1
		});

		// Right
		AddChild(_rects[3] = new ColorRect {
			Color = Color,
			AnchorLeft = 1,
			AnchorRight = 1,
			AnchorTop = 0,
			AnchorBottom = 1,
			OffsetRight = 1
		});
	}
}