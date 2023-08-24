using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition {
	public interface ISpriteDefinition {
		string Name { get; }
		Texture2D Texture { get; }
		Rect2 Region { get; }
		bool FlipHorizontal { get; }
		Color TransparentColor { get; }
		float TransparentColorThreshold { get; }
	}
}