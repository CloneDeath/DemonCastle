using System;
using DemonCastle.Game;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas.Dragging;

public partial class DraggableRegion : Control {
	private readonly DragData _drag = new();

	public CursorShape CursorShape { get; set; } = CursorShape.Drag;

	public bool IsSelected { get; set; }
	public event Action<DraggableRegion>? Selected;
	public event Action<DragData>? DragUpdate;

	public override void _Process(double delta) {
		base._Process(delta);

		if (IsSelected) {
			MouseDefaultCursorShape = CursorShape;

			if (Input.IsActionJustReleased(InputActions.EditorClick)) {
				_drag.Dragging = false;
			}

			if (!_drag.Dragging) return;
			_drag.MouseCurrent = (Vector2I)(GetGlobalMousePosition() / GetGlobalTransform().Scale);

			if (_drag.Delta.Length() <= 0) return;
			DragUpdate?.Invoke(_drag);
			_drag.MousePrevious = _drag.MouseCurrent;
		}
		else {
			MouseDefaultCursorShape = CursorShape.Arrow;
		}
	}

	public override void _GuiInput(InputEvent @event) {
		base._GuiInput(@event);

		if (!@event.IsActionPressed(InputActions.EditorClick)) return;
		if (IsSelected) {
			_drag.Dragging = true;
			_drag.MousePrevious = (Vector2I)(GetGlobalMousePosition() / GetGlobalTransform().Scale);
		} else {
			IsSelected = true;
			Selected?.Invoke(this);
		}
	}
}