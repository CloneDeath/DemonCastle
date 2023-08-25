using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects {
	public class ProjectManager {
		protected string GodotPath => "user://Projects/";
		protected string GlobalPath => Godot.ProjectSettings.GlobalizePath(GodotPath);
		protected FileCollection Files => new(GlobalPath);
		protected LocalProjectList LocalProjects => new();

		public async Task DownloadProjects() {
			if (Directory.Exists(GlobalPath)) {
				Directory.Delete(GlobalPath, true);
			}
			await DownloadProject("https://github.com/CloneDeath/HarmonyOfDespair/archive/refs/heads/master.zip");
			await DownloadProject("https://github.com/CloneDeath/PixelPlatformerExample/archive/refs/heads/master.zip");
		}
		
		public async Task DownloadProject(string url) {
			var dest = Path.GetTempFileName();
			using (var httpClient = new HttpClient()) {
				var responseBytes = await httpClient.GetByteArrayAsync(url);
				await File.WriteAllBytesAsync(dest, responseBytes);
			}
			ZipFile.ExtractToDirectory(dest, GlobalPath);
		}

		public bool ProjectsExist => GetProjects().Any();

		public IEnumerable<ProjectInfo> GetProjects() {
			var projectFiles = GetProjectFiles();
			foreach (var projectFile in projectFiles) {
				var fileNavigator = new FileNavigator<ProjectFile>(projectFile);
				yield return new ProjectInfo(fileNavigator);
			}
		}

		protected IEnumerable<string> GetProjectFiles() {
			return Files.ProjectFiles.Concat(LocalProjects.ProjectFiles);
		}

		public void ImportProject(string filePath) {
			LocalProjects.AddProject(filePath);
		}

		public void RemoveProject(ProjectInfo project) {
			LocalProjects.RemoveProject(project.FilePath);
		}
	}
}