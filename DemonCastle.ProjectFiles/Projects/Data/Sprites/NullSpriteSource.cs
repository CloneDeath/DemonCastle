using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class NullSpriteSource : ISpriteSource {
	public IEnumerable<ISpriteDefinition> Sprites => Array.Empty<ISpriteDefinition>();
}