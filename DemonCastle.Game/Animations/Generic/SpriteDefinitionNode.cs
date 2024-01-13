using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.Game.Animations.Generic;

public partial class SpriteDefinitionNode : Sprite2D {
	protected TransparentColorSpriteShader TransparentColorSpriteShader { get; }
	public SpriteDefinitionNode(ISpriteDefinition definition, Vector2I origin) {
		Name = $"{nameof(SpriteDefinitionNode)}-{definition.Name}";
		RegionEnabled = true;
		Material = TransparentColorSpriteShader = new TransparentColorSpriteShader();
		Offset = -origin;
		Centered = false;
		Load(definition);
	}

	protected void Load(ISpriteDefinition definition) {
		Texture = definition.Texture;
		RegionRect = definition.Region;
		FlipH = definition.FlipHorizontal;
		TransparentColorSpriteShader.TransparentColor = definition.TransparentColor;
		TransparentColorSpriteShader.Threshold = definition.TransparentColorThreshold;
	}
}