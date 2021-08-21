using System.Collections.Generic;
using System.Linq;
using DemonCastle.Exceptions;
using DemonCastle.Projects.Data;
using DemonCastle.Projects.Data.Levels;
using DemonCastle.Projects.Data.Sprites;
using Godot;
using Path = System.IO.Path;

namespace DemonCastle.Projects.Resources {
	public class DirectoryNavigator {
		protected string Directory { get; }
		protected ProjectResources ProjectResources { get; }

		public DirectoryNavigator(string directory) 
			: this(directory, new ProjectResources())
		{ }

		public DirectoryNavigator(string directory, ProjectResources resources) {
			Directory = directory;
			ProjectResources = resources;
		}

		public IEnumerable<CharacterInfo> GetCharacters(IEnumerable<string> localPaths) => localPaths.Select(GetCharacter);

		public CharacterInfo GetCharacter(string localPath) {
			var path = Path.Combine(Directory, localPath);
			return ProjectResources.GetCharacter(path);
		}

		public IEnumerable<LevelInfo> GetLevels(IEnumerable<string> localPaths) => localPaths.Select(GetLevel);

		public LevelInfo GetLevel(string localPath) {
			var path = Path.Combine(Directory, localPath);
			return ProjectResources.GetLevel(path);
		}

		public ISpriteInfo GetSprite(string localPath) {
			var path = Path.Combine(Directory, localPath);
			if (path.ToLower().EndsWith(".dcsg")) {
				return ProjectResources.GetSpriteGrid(path);
			}
			if (path.ToLower().EndsWith(".dcsa")) {
				return ProjectResources.GetSpriteAtlas(path);
			}
			throw new UnknownSpriteFileFormatException(path);
		}

		public Texture GetTexture(string localPath) {
			var path = Path.Combine(Directory, localPath);
			return ProjectResources.GetTexture(path);
		}
	}
}