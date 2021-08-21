using System.Collections.Generic;
using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Data.Levels;
using DemonCastle.Projects.Resources;

namespace DemonCastle.Projects.Data {
	public class ProjectInfo : IListableInfo {
		protected FileNavigator<ProjectFile> File { get; }
		protected ProjectFile Project => File.Resource;
		
		public ProjectInfo(FileNavigator<ProjectFile> file) {
			File = file;
		}

		public string Name => Project.Name;
		
		public IEnumerable<CharacterInfo> Characters => File.GetCharacters(Project.Characters);

		public IEnumerable<LevelInfo> Levels => File.GetLevels(Project.Levels);
		public string FilePath => File.FilePath;

		
		protected string GodotLocal => "user://";
		protected string GlobalPath => Godot.ProjectSettings.GlobalizePath(GodotLocal);

		public bool IsImported => !FilePath.StartsWith(GlobalPath);
	}
}