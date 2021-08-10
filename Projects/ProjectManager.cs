using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using DemonCastle.Projects.Data;

namespace DemonCastle.Projects {
	public class ProjectManager {
		protected string GodotPath => "user://Projects/";
		protected string GlobalPath => Godot.ProjectSettings.GlobalizePath(GodotPath);
		protected FileCollection Files => new FileCollection(GlobalPath);

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
			return projectFiles.Select(f => new ProjectInfo(f));
		}
	}
}