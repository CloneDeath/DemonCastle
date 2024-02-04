using System.Collections.Generic;
using DemonCastle.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.TileSets;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class ProjectInfo : FileInfo<ProjectFile>, IListableInfo {
	public ProjectInfo(FileNavigator<ProjectFile> file) : base(file) {
		Variables = new VariableDeclarationInfoCollection(file, Resource.Variables);
	}

	public string ListLabel => Name;

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
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

	public VariableDeclarationInfoCollection Variables { get; }

	public SceneInfo StartScene => File.GetScene(StartSceneFile);

	public IEnumerable<MonsterInfo> Monsters => File.GetFilesAndSubFiles()
												.Where(f => f.Extension == FileType.Monster.Extension)
												.Select(f => f.ToMonsterInfo());

	public IEnumerable<ItemInfo> Items => File.GetFilesAndSubFiles()
											  .Where(f => f.Extension == FileType.Item.Extension)
											  .Select(f => f.ToItemInfo());

	public IEnumerable<TileSetInfo> TileSets => File.GetFilesAndSubFiles()
													.Where(f => f.Extension == FileType.TileSet.Extension)
													.Select(f => f.ToTileSetInfo());

	public string FilePath => File.FilePath;
	public FileNavigator FileNavigator => File;


	protected static string GodotLocal => "user://";
	protected static string GlobalPath => Godot.ProjectSettings.GlobalizePath(GodotLocal);

	public bool IsImported => !FilePath.StartsWith(GlobalPath);
}