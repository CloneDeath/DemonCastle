using System.Collections.Generic;
using DemonCastle.Editor.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.Editor.FileInfo;

public class ExpandedDirectoriesList {
	private readonly FileNavigator<ProjectPreferencesFile> _file;
	private readonly ProjectPreferencesFile _resource;

	public ExpandedDirectoriesList(FileNavigator<ProjectPreferencesFile> file, ProjectPreferencesFile resource) {
		_resource = resource;
		_file = file;
	}

	public bool Contains(string directory) => _resource.ExpandedDirectories.Contains(directory);

	public void Clear() {
		_resource.ExpandedDirectories.Clear();
		_file.Save();
	}

	public void Add(string directory) {
		_resource.ExpandedDirectories.Add(directory);
		_file.Save();
	}

	public void AddRange(IEnumerable<string> directories) {
		_resource.ExpandedDirectories.AddRange(directories);
		_file.Save();
	}

	public void Remove(string directory) {
		_resource.ExpandedDirectories.Remove(directory);
		_file.Save();
	}
}