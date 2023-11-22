using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Game.Animations.Generic; 

public partial class SpriteDefinitionNode : Sprite2D {
	protected TransparentColorSpriteShader TransparentColorSpriteShader { get; }
	public SpriteDefinitionNode(ISpriteDefinition definition) {
		Texture = definition.Texture;
		RegionEnabled = true;
		RegionRect = definition.Region;
		FlipH = definition.FlipHorizontal;
		Material = TransparentColorSpriteShader = new TransparentColorSpriteShader {
			TransparentColor = definition.TransparentColor,
			Threshold = definition.TransparentColorThreshold
		};
		Offset = new Vector2(0, -definition.Region.Size.Y/2f);
	}
}