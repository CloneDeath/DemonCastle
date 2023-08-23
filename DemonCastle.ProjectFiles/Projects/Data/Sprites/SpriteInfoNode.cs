using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites {
	public partial class SpriteInfoNode : Sprite2D {
		protected TransparentColorSpriteShader TransparentColorSpriteShader { get; }
		public SpriteInfoNode(ISpriteDefinition definition) {
			Texture2D = definition.Texture2D;
			RegionEnabled = true;
			RegionRect = definition.Region;
			FlipH = definition.FlipHorizontal;
			Material = TransparentColorSpriteShader = new TransparentColorSpriteShader {
				TransparentColor = definition.TransparentColor,
				Threshold = definition.TransparentColorThreshold
			};
			Offset = new Vector2(0, -definition.Region.Size.y/2);
		}
	}
}