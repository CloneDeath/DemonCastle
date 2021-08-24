using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites {
	public class SpriteInfoNode : Sprite {
		protected TransparentColorSpriteShader TransparentColorSpriteShader { get; }
		public SpriteInfoNode(Texture texture, SpriteDefinition definition) {
			Texture = texture;
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