using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DemonCastle.Exceptions;
using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Data;

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

		public SpriteGridInfo GetSprite(string localPath) {
			var path = Path.Combine(Directory, localPath);
			if (path.ToLower().EndsWith(".dcsg")) {
				return ProjectResources.GetSpriteGrid(path);
			}
			throw new UnknownSpriteFileFormatException(path);
		}
	}
}