using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class ProjectInfo : FileInfo<ProjectFile>, IListableInfo {
	public ProjectInfo(FileNavigator<ProjectFile> file) : base(file) {
	}

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string Version {
		get => Resource.Version;
		set {
			Resource.Version = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string StartSceneFile {
		get => Resource.StartScene;
		set {
			Resource.StartScene = value;
			Save();
			OnPropertyChanged();
		}
	}


	public IEnumerable<CharacterInfo> Characters => File.GetFilesAndSubFiles()
														.Where(f => f.Extension == FileType.Character.Extension)
														.Select(f => f.ToCharacterInfo());

	public IEnumerable<LevelInfo> Levels => File.GetFilesAndSubFiles()
												.Where(f => f.Extension == FileType.Level.Extension)
												.Select(f => f.ToLevelInfo());

	public IEnumerable<MonsterInfo> Monsters => File.GetFilesAndSubFiles()
												.Where(f => f.Extension == FileType.Monster.Extension)
												.Select(f => f.ToMonsterInfo());

	public string FilePath => File.FilePath;
	public FileNavigator FileNavigator => File;


	protected static string GodotLocal => "user://";
	protected static string GlobalPath => Godot.ProjectSettings.GlobalizePath(GodotLocal);

	public bool IsImported => !FilePath.StartsWith(GlobalPath);
}