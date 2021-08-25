using System.Collections.Generic;
using System.IO;
using System.Linq;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;
using Path = System.IO.Path;

namespace DemonCastle.ProjectFiles.Projects.Resources {
	public class DirectoryNavigator {
		protected string Directory { get; }
		protected ProjectResources ProjectResources { get; }
		public string DirectoryName => new DirectoryInfo(Directory).Name;

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

		public TextInfo GetText(string localPath) {
			var path = Path.Combine(Directory, localPath);
			return ProjectResources.GetText(path);
		}

		public IEnumerable<DirectoryNavigator> GetDirectories() {
			var directories = System.IO.Directory.GetDirectories(Directory).OrderBy(s => s);
			return directories.Select(dir => new DirectoryNavigator(dir, ProjectResources));
		}

		public IEnumerable<FileNavigator> GetFiles() {
			var files = System.IO.Directory.GetFiles(Directory).OrderBy(s => s);
			return files.Select(file => new FileNavigator(file, ProjectResources));
		}
	}
}