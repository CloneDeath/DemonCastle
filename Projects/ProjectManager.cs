using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Data;
using DemonCastle.Projects.Resources;

namespace DemonCastle.Projects {
	public class ProjectManager {
		protected string GodotPath => "user://Projects/";
		protected string GlobalPath => Godot.ProjectSettings.GlobalizePath(GodotPath);
		protected FileCollection Files => new FileCollection(GlobalPath);
		protected LocalProjectList LocalProjects => new LocalProjectList();

		public void DownloadProjects() {
			if (Directory.Exists(GlobalPath)) {
				Directory.Delete(GlobalPath, true);
			}
			DownloadProject("https://github.com/CloneDeath/HarmonyOfDespair/archive/refs/heads/master.zip");
			DownloadProject("https://github.com/CloneDeath/PixelPlatformerExample/archive/refs/heads/master.zip");
		}
		
		public void DownloadProject(string url) {
			var dest = Path.GetTempFileName();
			using (var wc = new WebClient()) {
				wc.DownloadFile(url, dest);
			}
			ZipFile.ExtractToDirectory(dest, GlobalPath);
		}

		public bool ProjectsExist => GetProjects().Any();

		public IEnumerable<ProjectInfo> GetProjects() {
			var projectFiles = Files.ProjectFiles;
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
	}
}