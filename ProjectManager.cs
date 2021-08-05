using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using DemonCastle.ProjectFiles;
using Godot;
using Newtonsoft.Json;

namespace DemonCastle {
	public class ProjectManager {
		protected string GodotPath => "user://Games/";
		protected string GlobalPath => ProjectSettings.GlobalizePath(GodotPath);
		
		public void DownloadProjects() {
			const string url = "https://github.com/CloneDeath/PixelPlatformerExample/archive/refs/heads/master.zip";
			var dest = System.IO.Path.GetTempFileName();
			using (var wc = new WebClient()) {
				wc.DownloadFile(url, dest);
			}

			System.IO.Directory.Delete(GlobalPath, true);
			ZipFile.ExtractToDirectory(dest, GlobalPath);
		}

		public List<ProjectFile> GetProjects() {
			var projectFiles = GetProjectFiles(GlobalPath);
			return projectFiles.Select(AsProjectFile).ToList();
		}

		protected ProjectFile AsProjectFile(string arg) {
			var contents = System.IO.File.ReadAllText(arg);
			return JsonConvert.DeserializeObject<ProjectFile>(contents);
		}

		protected IEnumerable<string> GetProjectFiles(string path) {
			var files = System.IO.Directory.GetFiles(path);
			var relevantFiles = files.Where(f => f.ToLower().EndsWith(".dcp"));

			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (var subDir in System.IO.Directory.GetDirectories(path)) {
				relevantFiles = relevantFiles.Concat(GetProjectFiles(subDir));
			}

			return relevantFiles;
		}
	}
}