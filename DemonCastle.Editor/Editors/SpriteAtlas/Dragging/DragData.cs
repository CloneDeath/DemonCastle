using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas.Dragging;

public class DragData {
	public bool Dragging { get; set; }
	public Vector2I PositionStart { get; set; }
	public Vector2I MouseStart { get; set; }

	public Vector2I MouseCurrent { get; set; }

	public Vector2I NewPosition => PositionStart + (MouseCurrent - MouseStart);
}