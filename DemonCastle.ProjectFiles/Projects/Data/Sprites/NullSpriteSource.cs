using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class NullSpriteSource : ISpriteSource {
	public ISpriteDefinition GetSpriteDefinition(string spriteName) {
		return new NullSpriteDefinition();
	}

	public IEnumerable<string> SpriteNames => Array.Empty<string>();
}