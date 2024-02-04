using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using File = System.IO.File;
using Path = System.IO.Path;

namespace DemonCastle.Navigation;

public class DirectoryNavigator {
	public string Directory { get; protected set; }
	public string DirectoryName => new DirectoryInfo(Directory).Name;

	public DirectoryNavigator(string directory) {
		Directory = directory;
	}

	// public AudioStream GetAudioStream(string localPath) {
	// 	var path = ToAbsolutePath(localPath);
	// 	return ProjectResources.GetAudioStream(path);
	// }
	//
	// public CharacterInfo GetCharacter(string localPath) {
	// 	var path = ToAbsolutePath(localPath);
	// 	return ProjectResources.GetCharacter(path);
	// }
	//
	// public IEnumerable<CharacterInfo> GetCharacters(IEnumerable<string> localPaths) => localPaths.Select(GetCharacter);
	//
	// public Font GetFont(string localPath) {
	// 	var path = ToAbsolutePath(localPath);
	// 	return ProjectResources.GetFont(path);
	// }
	//
	// public LevelInfo GetLevel(string localPath) {
	// 	var path = ToAbsolutePath(localPath);
	// 	return ProjectResources.GetLevel(path);
	// }
	//
	// public SceneInfo GetScene(string localPath) {
	// 	var path = ToAbsolutePath(localPath);
	// 	return ProjectResources.GetScene(path);
	// }
	//
	// public ISpriteSource GetSprite(string localPath) {
	// 	var path = ToAbsolutePath(localPath);
	// 	if (path.ToLower().EndsWith(FileType.SpriteGrid.Extension)) {
	// 		return ProjectResources.GetSpriteGrid(path);
	// 	}
	// 	if (path.ToLower().EndsWith(FileType.SpriteAtlas.Extension)) {
	// 		return ProjectResources.GetSpriteAtlas(path);
	// 	}
	// 	throw new UnknownSpriteFileFormatException(path);
	// }
	//
	// public TextInfo GetText(string localPath) {
	// 	var path = ToAbsolutePath(localPath);
	// 	return ProjectResources.GetText(path);
	// }
	//
	// public TileSetInfo GetTileSet(string localPath) {
	// 	var path = ToAbsolutePath(localPath);
	// 	return ProjectResources.GetTileSet(path);
	// }
	//
	// public TileSetInfo GetTileSet(Guid tileSetId) => ProjectResources.GetTileSet(tileSetId);
	//
	// public Texture2D GetTexture(string localPath) {
	// 	var path = ToAbsolutePath(localPath);
	// 	return ProjectResources.GetTexture(path);
	// }
	//
	// public WeaponInfo GetWeapon(string localPath) {
	// 	var path = ToAbsolutePath(localPath);
	// 	return ProjectResources.GetWeapon(path);
	// }

	public IEnumerable<FileNavigator> GetFilesAndSubFiles() {
		var childSubFiles = GetDirectories().SelectMany(d => d.GetFilesAndSubFiles());
		return GetFiles().Concat(childSubFiles);
	}

	public IEnumerable<DirectoryNavigator> GetDirectories() {
		var directories = System.IO.Directory.GetDirectories(Directory).OrderBy(s => s);
		return directories.Select(dir => new DirectoryNavigator(dir));
	}

	public IEnumerable<FileNavigator> GetFiles() {
		var files = System.IO.Directory.GetFiles(Directory).OrderBy(s => s);
		return files.Select(file => new FileNavigator(file));
	}

	public void CreateDirectory(string directoryName) {
		var fullPath = ToAbsolutePath(directoryName);
		System.IO.Directory.CreateDirectory(fullPath);
	}

	public void DeleteDirectory() => System.IO.Directory.Delete(Directory, true);

	public void RenameDirectory(string newName) {
		var parent = System.IO.Directory.GetParent(Directory)
					 ?? throw new NullReferenceException();
		var fullPath = Path.GetFullPath(Path.Combine(parent.FullName, newName));
		System.IO.Directory.Move(Directory, fullPath);
		Directory = fullPath;
	}

	public void CreateFile(string fileName, string extension, string contents) {
		var file = $"{fileName}{extension}";
		if (FileExists(file)) {
			CreateFile(fileName, 0, extension, contents);
		} else {
			CreateFileWithContents(file, contents);
		}
	}

	public void CreateEmptyFile(string fileName, string extension) {
		var file = $"{fileName}{extension}";
		var contents = string.Empty;
		if (FileExists(file)) {
			CreateFile(fileName, 0, extension, contents);
		} else {
			CreateFileWithContents(file, contents);
		}
	}

	protected void CreateFile(string fileName, int index, string extension, string contents) {
		while (true) {
			var file = $"{fileName}{index}{extension}";
			if (FileExists(file)) {
				index += 1;
				continue;
			}

			CreateFileWithContents(file, contents);
			break;
		}
	}

	protected void CreateFileWithContents(string fileName, string contents) {
		var fullPath = ToAbsolutePath(fileName);
		File.WriteAllText(fullPath, contents);
	}

	public bool FileExists(string fileName) {
		if (string.IsNullOrWhiteSpace(fileName)) {
			return false;
		}

		var fullPath = ToAbsolutePath(fileName);
		return File.Exists(fullPath);
	}

	private string ToAbsolutePath(string fileName) {
		var fullPath = Path.Combine(Directory, fileName);
		return Path.GetFullPath(fullPath);
	}
}