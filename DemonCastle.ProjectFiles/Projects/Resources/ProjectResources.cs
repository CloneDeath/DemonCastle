using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Resources {
	public class ProjectResources {
		protected FileNavigator<T> GetFile<T>(string path) => new FileNavigator<T>(path, this);

		public ProjectResources() {
			Characters = new ResourceCache<CharacterInfo>(path
				=> new CharacterInfo(GetFile<CharacterFile>(path)));
			
			Levels = new ResourceCache<LevelInfo>(path
				=> new LevelInfo(GetFile<LevelFile>(path)));

			SpriteGrids = new ResourceCache<SpriteGridInfo>(path
				=> new SpriteGridInfo(GetFile<SpriteGridFile>(path)));

			SpriteAtlases = new ResourceCache<SpriteAtlasInfo>(path
				=> new SpriteAtlasInfo(GetFile<SpriteAtlasFile>(path)));

			Textures = new ResourceCache<Texture>(path
				=> {
				var image = new Image();
				image.Load(path);

				var texture = new ImageTexture();
				texture.CreateFromImage(image);
				return texture;
			});
		}
		protected ResourceCache<CharacterInfo> Characters { get; } 
		public CharacterInfo GetCharacter(string path) => Characters.Get(path);

		protected ResourceCache<LevelInfo> Levels { get; }
		public LevelInfo GetLevel(string path) => Levels.Get(path);

		protected ResourceCache<SpriteGridInfo> SpriteGrids { get; }
		public SpriteGridInfo GetSpriteGrid(string path) => SpriteGrids.Get(path);

		protected ResourceCache<Texture> Textures { get; }
		public Texture GetTexture(string path) => Textures.Get(path);

		protected ResourceCache<SpriteAtlasInfo> SpriteAtlases { get; }
		public SpriteAtlasInfo GetSpriteAtlas(string path) => SpriteAtlases.Get(path);
	}
}