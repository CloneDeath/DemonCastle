using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites {
	public partial class NullSpriteDefinition : ISpriteDefinition {
		public string Name => "null";
		public Texture2D Texture2D => new GradientTexture2D {
			Gradient = new Gradient {
				Colors = new []{Colors.Red, Colors.Blue},
				Offsets = new []{0f, 1f}
			},
			Width = 16
		};
		public Rect2 Region => new Rect2(0, 0, 16, 16);
		public bool FlipHorizontal => false;
		public Color TransparentColor => Colors.Transparent;
		public float TransparentColorThreshold => 0.01f;
	}
}