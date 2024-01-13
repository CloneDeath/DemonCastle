using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class NullSpriteSource : ISpriteSource {
	public IEnumerableInfo<ISpriteDefinition> Sprites => new NullEnumerableInfo<ISpriteDefinition>();
}