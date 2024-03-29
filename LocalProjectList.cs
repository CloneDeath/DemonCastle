using System.Collections.Generic;
using System.IO;
using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects;

namespace DemonCastle;

public static class LocalProjectList {
	private static string GodotPath => "user://ProjectList.json";
	private static string GlobalPath => Godot.ProjectSettings.GlobalizePath(GodotPath);

	private static ProjectListFile GetProjectList() {
		if (!File.Exists(GlobalPath)) return new ProjectListFile();

		var contents = File.ReadAllText(GlobalPath);
		return Serializer.Deserialize<ProjectListFile>(contents);
	}

	public static IEnumerable<string> ProjectFiles => GetProjectList().Projects;

	public static void AddProject(string filePath) {
		var project = GetProjectList();
		project.Projects.Add(filePath);
		SaveProjectList(project);
	}

	private static void SaveProjectList(ProjectListFile project) {
		project.Projects = project.Projects.Where(File.Exists).Distinct().ToList();
		var content = Serializer.Serialize(project);
		File.WriteAllText(GlobalPath, content);
	}

	public static void RemoveProject(string projectFilePath) {
		var project = GetProjectList();
		project.Projects.Remove(projectFilePath);
		SaveProjectList(project);
	}
}