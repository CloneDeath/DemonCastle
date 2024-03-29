using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using DemonCastle.Files;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using HttpClient = System.Net.Http.HttpClient;
using Version = DemonCastle.ProjectFiles.Version;

namespace DemonCastle;

public class SampleProject {
	public string Name { get; }
	public string ProjectFile { get; }
	public string Url { get; }

	public SampleProject(string name, string projectFile, string url) {
		Name = name;
		ProjectFile = projectFile;
		Url = url;
	}

	public async Task DownloadProject(string destination) {
		var zipFile = Path.GetTempFileName();
		using (var httpClient = new HttpClient()) {
			var responseBytes = await httpClient.GetByteArrayAsync(Url);
			await File.WriteAllBytesAsync(zipFile, responseBytes);
		}
		ZipFile.ExtractToDirectory(zipFile, destination);
	}
}

public class ProjectManager {
	public static List<SampleProject> SampleProjects { get; } = new() {
		new SampleProject("Devil Castle Dracula 1", "DevilCastleDracula1.dcp",
			"https://github.com/CloneDeath/DevilCastleDracula1/archive/refs/heads/master.zip"),
		new SampleProject("Pixel Platformer Example", "PixelPlatformer.dcp",
			"https://github.com/CloneDeath/PixelPlatformerExample/archive/refs/heads/master.zip"),
		new SampleProject("Harmony of Despair", "HarmonyOfDespair.dcp",
			"https://github.com/CloneDeath/HarmonyOfDespair/archive/refs/heads/master.zip")
	};

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
		return LocalProjectList.ProjectFiles;
	}

	public static void ImportProject(string filePath) {
        LocalProjectList.AddProject(filePath);
	}

	public static void RemoveProject(ProjectInfo project) {
        LocalProjectList.RemoveProject(project.FilePath);
	}

	public static async Task<ProjectWithResources> CreateSample(SampleProject sample, string folderPath) {
		if (!Directory.Exists(folderPath)) throw new Exception($"Folder '{folderPath}' does not exist.");
		if (Directory.EnumerateFiles(folderPath).Any() || Directory.EnumerateDirectories(folderPath).Any()) {
			throw new Exception("Folder must be empty.");
		}

		await sample.DownloadProject(folderPath);

		var projectFilePath = Path.Combine(folderPath, sample.ProjectFile);
		LocalProjectList.AddProject(projectFilePath);

		var resources = new ProjectResources(Path.GetDirectoryName(projectFilePath) ?? throw new DirectoryNotFoundException());
		var project = resources.GetProject(projectFilePath);
		return new ProjectWithResources(resources, project);
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