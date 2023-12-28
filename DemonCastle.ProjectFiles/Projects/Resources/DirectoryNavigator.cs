using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;
using File = System.IO.File;
using Path = System.IO.Path;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public class DirectoryNavigator {
	public string Directory { get; protected set; }
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

	public WeaponInfo GetWeapon(string localPath) {
		var path = Path.Combine(Directory, localPath);
		return ProjectResources.GetWeapon(path);
	}

	public Font GetFont(string localPath) {
		var path = Path.Combine(Directory, localPath);
		return ProjectResources.GetFont(path);
	}

	public IEnumerable<LevelInfo> GetLevels(IEnumerable<string> localPaths) => localPaths.Select(GetLevel);

	public LevelInfo GetLevel(string localPath) {
		var path = Path.Combine(Directory, localPath);
		return ProjectResources.GetLevel(path);
	}

	public ISpriteSource GetSprite(string localPath) {
		var path = Path.Combine(Directory, localPath);
		if (path.ToLower().EndsWith(".dcsg")) {
			return ProjectResources.GetSpriteGrid(path);
		}
		if (path.ToLower().EndsWith(".dcsa")) {
			return ProjectResources.GetSpriteAtlas(path);
		}
		throw new UnknownSpriteFileFormatException(path);
	}

	public AudioStream GetAudioStream(string localPath) {
		var path = Path.Combine(Directory, localPath);
		return ProjectResources.GetAudioStream(path);
	}

	public Texture2D GetTexture(string localPath) {
		var path = Path.Combine(Directory, localPath);
		return ProjectResources.GetTexture(path);
	}

	public TextInfo GetText(string localPath) {
		var path = Path.Combine(Directory, localPath);
		return ProjectResources.GetText(path);
	}

	public IEnumerable<FileNavigator> GetFilesAndSubFiles() {
		var childSubFiles = GetDirectories().SelectMany(d => d.GetFilesAndSubFiles());
		return GetFiles().Concat(childSubFiles);
	}

	public IEnumerable<DirectoryNavigator> GetDirectories() {
		var directories = System.IO.Directory.GetDirectories(Directory).OrderBy(s => s);
		return directories.Select(dir => new DirectoryNavigator(dir, ProjectResources));
	}

	public IEnumerable<FileNavigator> GetFiles() {
		var files = System.IO.Directory.GetFiles(Directory).OrderBy(s => s);
		return files.Select(file => new FileNavigator(file, ProjectResources));
	}

	public void CreateDirectory(string directoryName) {
		var fullPath = Path.Combine(Directory, directoryName);
		System.IO.Directory.CreateDirectory(fullPath);
	}

	public void DeleteDirectory() => System.IO.Directory.Delete(Directory, true);

	public void RenameDirectory(string newName) {
		var parent = System.IO.Directory.GetParent(Directory)
					 ?? throw new NullReferenceException();
		var fullPath = Path.Combine(parent.FullName, newName);
		System.IO.Directory.Move(Directory, fullPath);
		Directory = fullPath;
	}

	public void CreateFile<TFile>(string fileName, string extension, TFile data) {
		var file = $"{fileName}{extension}";
		var contents = Serializer.Serialize(data);
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
		var fullPath = Path.Combine(Directory, fileName);
		File.WriteAllText(fullPath, contents);
	}

	public bool FileExists(string fileName) {
		if (string.IsNullOrWhiteSpace(fileName)) {
			return false;
		}
		var fullPath = Path.Combine(Directory, fileName);
		return File.Exists(fullPath);
	}
}