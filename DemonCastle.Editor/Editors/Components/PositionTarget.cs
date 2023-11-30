using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class PositionTarget : Control {
	private const int _length = 3;
	private readonly Color _color = Colors.White;

	private ColorRect[] _lines = new ColorRect[4];

	public Vector2 Target {
		get {
			var offset = new Vector2(_length + 1, _length + 1);
			return Position - offset;
		}
		set {
			var offset = new Vector2(_length + 1, _length + 1);
			Position = value - offset;
		}
	}

	public PositionTarget() {
		Name = nameof(PositionTarget);
		CustomMinimumSize = new Vector2(_length * 2 + 3,  _length * 2 + 3);
		MouseFilter = MouseFilterEnum.Pass;

		// Top
		AddChild(_lines[0] = new ColorRect {
			Size = new Vector2(1, _length),
			Color = _color,
			MouseFilter = MouseFilterEnum.Pass
		});
		_lines[0].SetAnchorsPreset(LayoutPreset.CenterTop);
		_lines[0].Position = new Vector2(-1, 0);

		// Bottom
		AddChild(_lines[1] = new ColorRect {
			Size = new Vector2(1, _length),
			Color = _color,
			MouseFilter = MouseFilterEnum.Pass
		});
		_lines[1].SetAnchorsPreset(LayoutPreset.CenterBottom);
		_lines[1].Position = new Vector2(-1, -_length);

		// Left
		AddChild(_lines[2] = new ColorRect {
			Size = new Vector2(_length, 1),
			Color = _color,
			MouseFilter = MouseFilterEnum.Pass
		});
		_lines[2].SetAnchorsPreset(LayoutPreset.CenterLeft);
		_lines[2].Position = new Vector2(0, -1);


		// Right
		AddChild(_lines[3] = new ColorRect {
			Size = new Vector2(_length, 1),
			Color = _color,
			MouseFilter = MouseFilterEnum.Pass
		});
		_lines[3].SetAnchorsPreset(LayoutPreset.CenterRight);
		_lines[3].Position = new Vector2(-_length, -1);
	}
}