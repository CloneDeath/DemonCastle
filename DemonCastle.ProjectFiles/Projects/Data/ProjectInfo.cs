using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class ProjectInfo : IListableInfo {
	public FileNavigator<ProjectFile> File { get; }
	protected ProjectFile Project => File.Resource;
	public string FileName => File.FileName;

	public ProjectInfo(FileNavigator<ProjectFile> file) {
		File = file;
	}

	public string Name => Project.Name;

	public IEnumerable<CharacterInfo> Characters => File.GetFilesAndSubFiles()
														.Where(f => f.Extension == FileType.Character.Extension)
														.Select(f => f.ToCharacterInfo());

	public IEnumerable<LevelInfo> Levels => File.GetFilesAndSubFiles()
												.Where(f => f.Extension == FileType.Level.Extension)
												.Select(f => f.ToLevelInfo());
	public string FilePath => File.FilePath;


	protected static string GodotLocal => "user://";
	protected static string GlobalPath => Godot.ProjectSettings.GlobalizePath(GodotLocal);

	public bool IsImported => !FilePath.StartsWith(GlobalPath);
}