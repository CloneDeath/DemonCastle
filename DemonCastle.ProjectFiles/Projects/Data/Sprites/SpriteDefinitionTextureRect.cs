using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites; 

public partial class SpriteDefinitionTextureRect : TextureRect {
	public SpriteDefinitionTextureRect(ISpriteDefinition definition) {
		Texture = new AtlasTexture {
			Atlas = definition.Texture,
			Region = definition.Region,
			FilterClip = true
		};
		FlipH = definition.FlipHorizontal;
	}
}