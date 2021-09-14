using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites {
	public interface ISpriteSource {
		ISpriteDefinition GetSpriteDefinition(string spriteName);
	}
}