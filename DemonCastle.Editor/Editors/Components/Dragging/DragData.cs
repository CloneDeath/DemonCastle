using Godot;

namespace DemonCastle.Editor.Editors.Components.Dragging;

public class DragData {
	public bool Dragging { get; set; }

	public Vector2I MousePrevious { get; set; }
	public Vector2I MouseCurrent { get; set; }

	public Vector2I Delta => MouseCurrent - MousePrevious;
}