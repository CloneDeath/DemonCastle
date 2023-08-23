using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites {
	public partial class NullSpriteSource : ISpriteSource {
		public ISpriteDefinition GetSpriteDefinition(string spriteName) {
			return new NullSpriteDefinition();
		}
	}
}