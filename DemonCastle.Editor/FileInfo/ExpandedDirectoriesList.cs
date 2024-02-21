using System.Collections.Generic;
using System.Linq;
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
		Save();
	}

	public void Add(string directory) {
		_resource.ExpandedDirectories.Add(directory);
		Save();
	}

	public void AddRange(IEnumerable<string> directories) {
		_resource.ExpandedDirectories.AddRange(directories);
		Save();
	}

	public void Remove(string directory) {
		_resource.ExpandedDirectories.RemoveAll(d => d == directory);
		Save();
	}

	private void Save() {
		_resource.ExpandedDirectories = _resource.ExpandedDirectories.Distinct().ToList();
		_file.Save();
	}
}