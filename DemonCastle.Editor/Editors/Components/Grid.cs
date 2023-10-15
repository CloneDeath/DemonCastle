using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class Grid : Control {
	private Vector2I _cellSize;
	private Color _color = Colors.White;

	public Vector2I CellSize {
		get => _cellSize;
		set {
			_cellSize = value;
			QueueRedraw();
		}
	}

	public Color Color {
		get => _color;
		set {
			_color = value;
			QueueRedraw();
		}
	}

	public override void _Draw() {
		base._Draw();

		for (var x = _cellSize.X; x < Size.X; x += _cellSize.X) {
			DrawLine(new Vector2(x, 0), new Vector2(x, Size.Y), _color);
		}

		for (var y = _cellSize.Y; y < Size.Y; y += _cellSize.Y) {
			DrawLine(new Vector2(0, y), new Vector2(Size.X, y), _color);
		}
	}

	public Grid() {
		Resized += OnResized;
	}

	private void OnResized() {
		QueueRedraw();
	}
}