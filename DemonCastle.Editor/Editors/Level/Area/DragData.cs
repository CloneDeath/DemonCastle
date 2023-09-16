using Godot;

namespace DemonCastle.Editor.Editors.Level.Area; 

public class DragData {
	public bool Dragging { get; set; }
	public Vector2 PositionStart { get; set; }
	public Vector2 MouseStart { get; set; }
	
	public Vector2 MouseCurrent { get; set; }

	public Vector2 NewPosition => PositionStart + (MouseCurrent - MouseStart);
}