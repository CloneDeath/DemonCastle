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
			const string url = "https://github.com/CloneDeath/PixelPlatformerExample/archive/refs/heads/master.zip";
			var dest = Path.GetTempFileName();
			using (var wc = new WebClient()) {
				wc.DownloadFile(url, dest);
			}

			if (Directory.Exists(GlobalPath)) {
				Directory.Delete(GlobalPath, true);
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