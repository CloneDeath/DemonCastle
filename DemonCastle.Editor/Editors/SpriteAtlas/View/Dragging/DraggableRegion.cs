using System;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Game;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas.View.Dragging;

public partial class DraggableRegion : SelectableControl {
	private readonly DragData _drag = new();

	public event Action<DragData>? DragUpdate;

	public DraggableRegion() {
		SelectedCursorShape = CursorShape.Drag;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		if (!IsSelected) return;

		if (Input.IsActionJustReleased(InputActions.EditorClick)) {
			_drag.Dragging = false;
		}
		if (!_drag.Dragging) return;

		_drag.MouseCurrent = (Vector2I)(GetGlobalMousePosition() / GetGlobalTransform().Scale);

		if (_drag.Delta.Length() <= 0) return;
		DragUpdate?.Invoke(_drag);
		_drag.MousePrevious = _drag.MouseCurrent;
	}

	public override void _GuiInput(InputEvent @event) {
		base._GuiInput(@event);

		if (!@event.IsActionPressed(InputActions.EditorClick)) return;
		if (!IsSelected) return;

		_drag.Dragging = true;
		_drag.MousePrevious = (Vector2I)(GetGlobalMousePosition() / GetGlobalTransform().Scale);
	}
}