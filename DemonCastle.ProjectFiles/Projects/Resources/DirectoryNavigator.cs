using System.Collections.Generic;
using System.IO;
using System.Linq;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;
using Newtonsoft.Json;
using File = System.IO.File;
using Path3D = System.IO.Path3D;

namespace DemonCastle.ProjectFiles.Projects.Resources {
	public partial class DirectoryNavigator {
		public string DirAccess { get; }
		protected ProjectResources ProjectResources { get; }
		public string DirectoryName => new DirectoryInfo(DirAccess).Name;

		public DirectoryNavigator(string directory) 
			: this(directory, new ProjectResources())
		{ }

		public DirectoryNavigator(string directory, ProjectResources resources) {
			DirAccess = directory;
			ProjectResources = resources;
		}

		public IEnumerable<CharacterInfo> GetCharacters(IEnumerable<string> localPaths) => localPaths.Select(GetCharacter);

		public CharacterInfo GetCharacter(string localPath) {
			var path = Path3D.Combine(DirAccess, localPath);
			return ProjectResources.GetCharacter(path);
		}

		public IEnumerable<LevelInfo> GetLevels(IEnumerable<string> localPaths) => localPaths.Select(GetLevel);

		public LevelInfo GetLevel(string localPath) {
			var path = Path3D.Combine(DirAccess, localPath);
			return ProjectResources.GetLevel(path);
		}

		public ISpriteSource GetSprite(string localPath) {
			var path = Path3D.Combine(DirAccess, localPath);
			if (path.ToLower().EndsWith(".dcsg")) {
				return ProjectResources.GetSpriteGrid(path);
			}
			if (path.ToLower().EndsWith(".dcsa")) {
				return ProjectResources.GetSpriteAtlas(path);
			}
			throw new UnknownSpriteFileFormatException(path);
		}

		public Texture2D GetTexture(string localPath) {
			var path = Path3D.Combine(DirAccess, localPath);
			return ProjectResources.GetTexture(path);
		}

		public TextInfo GetText(string localPath) {
			var path = Path3D.Combine(DirAccess, localPath);
			return ProjectResources.GetText(path);
		}

		public IEnumerable<FileNavigator> GetFilesAndSubFiles() {
			var childSubFiles = GetDirectories().SelectMany(d => d.GetFilesAndSubFiles());
			return GetFiles().Concat(childSubFiles);
		}

		public IEnumerable<DirectoryNavigator> GetDirectories() {
			var directories = System.IO.DirAccess.GetDirectories(DirAccess).OrderBy(s => s);
			return directories.Select(dir => new DirectoryNavigator(dir, ProjectResources));
		}

		public IEnumerable<FileNavigator> GetFiles() {
			var files = System.IO.DirAccess.GetFiles(DirAccess).OrderBy(s => s);
			return files.Select(file => new FileNavigator(file, ProjectResources));
		}

		public void CreateFile<TFile>(string fileName, string extension, TFile data) {
			var file = $"{fileName}.{extension}";
			var contents = JsonConvert.SerializeObject(data);
			if (FileExists(file)) {
				CreateFile(fileName, 0, extension, contents);
			}
			else {
				CreateFileWithContents(file, contents);
			}
		}

		protected void CreateFile(string fileName, int index, string extension, string contents) {
			while (true) {
				var file = $"{fileName}{index}.{extension}";
				if (FileExists(file)) {
					index += 1;
					continue;
				}

				CreateFileWithContents(file, contents);
				break;
			}
		}

		protected void CreateFileWithContents(string fileName, string contents) {
			var fullPath = Path3D.Combine(DirAccess, fileName);
			File.WriteAllText(fullPath, contents);
		}

		public bool FileExists(string fileName) {
			var fullPath = Path3D.Combine(DirAccess, fileName);
			return File.Exists(fullPath);
		}
	}
}