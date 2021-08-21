using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites {
	public class SpriteDefinition {
		public Rect2 Region { get; set; }
		public bool FlipHorizontal { get; set; } = false;
		public Color TransparentColor { get; set; } = Colors.Transparent;
		public float TransparentColorThreshold { get; set; } = 0;
	}
}