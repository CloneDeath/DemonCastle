using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Data;

namespace DemonCastle.Projects.Resources {
	public class ProjectResources {
		protected FileNavigator<T> GetFile<T>(string path) => new FileNavigator<T>(path, this);

		public ProjectResources() {
			Characters = new ResourceCache<CharacterInfo>(path
				=> new CharacterInfo(GetFile<CharacterFile>(path)));
			
			Levels = new ResourceCache<LevelInfo>(path
				=> new LevelInfo(GetFile<LevelFile>(path)));

			SpriteGrids = new ResourceCache<SpriteGridInfo>(path
				=> new SpriteGridInfo(GetFile<SpriteGridFile>(path)));
		}
		protected ResourceCache<CharacterInfo> Characters { get; } 
		public CharacterInfo GetCharacter(string path) => Characters.Get(path);

		protected ResourceCache<LevelInfo> Levels { get; }
		public LevelInfo GetLevel(string path) => Levels.Get(path);

		protected ResourceCache<SpriteGridInfo> SpriteGrids { get; }
		public SpriteGridInfo GetSpriteGrid(string path) => SpriteGrids.Get(path);
	}
}