using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;
using HttpClient = System.Net.Http.HttpClient;

namespace DemonCastle.ProjectFiles.Projects;

public class ProjectManager {
	protected const string GodotPath = "user://Projects/";
	protected static string GlobalPath => ProjectSettings.GlobalizePath(GodotPath);
	protected static FileCollection Files => new(GlobalPath);
	protected static LocalProjectList LocalProjects => new();

	public static async Task DownloadProjects() {
		if (Directory.Exists(GlobalPath)) {
			Directory.Delete(GlobalPath, true);
		}
		await DownloadProject("https://github.com/CloneDeath/DemonCastle/archive/refs/heads/master.zip");
		await DownloadProject("https://github.com/CloneDeath/PixelPlatformerExample/archive/refs/heads/master.zip");
		await DownloadProject("https://github.com/CloneDeath/HarmonyOfDespair/archive/refs/heads/master.zip");
	}

	public static async Task DownloadProject(string url) {
		var dest = Path.GetTempFileName();
		using (var httpClient = new HttpClient()) {
			var responseBytes = await httpClient.GetByteArrayAsync(url);
			await File.WriteAllBytesAsync(dest, responseBytes);
		}
		ZipFile.ExtractToDirectory(dest, GlobalPath);
	}

	public static bool ProjectsExist => GetProjects().Any();

	public static IEnumerable<ProjectWithResources> GetProjects() {
		var projectFiles = GetProjectFiles().Where(File.Exists).Distinct();
		foreach (var projectFile in projectFiles) {
			var resources = new ProjectResources(Path.GetDirectoryName(projectFile) ?? throw new DirectoryNotFoundException());
			var project = resources.GetProject(projectFile);
			yield return new ProjectWithResources(resources, project);
		}
	}

	protected static IEnumerable<string> GetProjectFiles() {
		return Files.ProjectFiles.Concat(LocalProjectList.ProjectFiles);
	}

	public static void ImportProject(string filePath) {
        LocalProjectList.AddProject(filePath);
	}

	public static void RemoveProject(ProjectInfo project) {
        LocalProjectList.RemoveProject(project.FilePath);
	}

	public static ProjectWithResources CreateProject(string folderPath) {
		if (!Directory.Exists(folderPath)) throw new Exception($"Folder '{folderPath}' does not exist.");
		if (Directory.EnumerateFiles(folderPath).Any() || Directory.EnumerateDirectories(folderPath).Any()) {
			throw new Exception("Folder must be empty.");
		}

		var projectName = Path.GetFileName(folderPath)
						  ?? throw new Exception($"Failed to get Folder Name from '{folderPath}'");
		var projectFilePath = Path.Combine(folderPath, $"{projectName}.dcp");

		var projectFile = new ProjectFile {
			Name = projectName,
			Version = Version.Current
		};
		File.WriteAllText(projectFilePath, Serializer.Serialize(projectFile));

        LocalProjectList.AddProject(projectFilePath);

		var resources = new ProjectResources(Path.GetDirectoryName(projectFilePath) ?? throw new DirectoryNotFoundException());
		var project = resources.GetProject(projectFilePath);
		return new ProjectWithResources(resources, project);
	}
}