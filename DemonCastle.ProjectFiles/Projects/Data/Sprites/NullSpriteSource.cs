using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class NullSpriteSource : ISpriteSource {
	public IEnumerableInfo<ISpriteDefinition> Sprites => new NullEnumerableInfo<ISpriteDefinition>();
}