using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public interface ISpriteSource {
	IEnumerableInfo<ISpriteDefinition> Sprites { get; }
}