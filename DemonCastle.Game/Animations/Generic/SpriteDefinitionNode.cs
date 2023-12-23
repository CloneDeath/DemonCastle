using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Game.Animations.Generic;

public partial class SpriteDefinitionNode : Sprite2D {
	protected TransparentColorSpriteShader TransparentColorSpriteShader { get; }
	public SpriteDefinitionNode(ISpriteDefinition definition, Vector2I origin) {
		Name = $"{nameof(SpriteDefinitionNode)}-{definition.Name}";
		Texture = definition.Texture;
		RegionEnabled = true;
		RegionRect = definition.Region;
		FlipH = definition.FlipHorizontal;
		Material = TransparentColorSpriteShader = new TransparentColorSpriteShader {
			TransparentColor = definition.TransparentColor,
			Threshold = definition.TransparentColorThreshold
		};
		Offset = -origin;
		Centered = false;
	}
}