using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using DemonCastle.ProjectFiles;
using Newtonsoft.Json;

namespace DemonCastle {
	public class ProjectManager {
		protected string GodotPath => "user://Projects/";
		protected string GlobalPath => Godot.ProjectSettings.GlobalizePath(GodotPath);
		
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

		public IEnumerable<ProjectFile> GetProjects() {
			var projectFiles = GetProjectFiles(GlobalPath);
			return projectFiles.Select(AsProjectFile);
		}

		protected ProjectFile AsProjectFile(string arg) {
			var contents = File.ReadAllText(arg);
			return JsonConvert.DeserializeObject<ProjectFile>(contents);
		}

		protected IEnumerable<string> GetProjectFiles(string path) {
			if (!Directory.Exists(path)) return new string[0];
			
			var files = Directory.GetFiles(path);
			var relevantFiles = files.Where(f => f.ToLower().EndsWith(".dcp"));

			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (var subDir in Directory.GetDirectories(path)) {
				relevantFiles = relevantFiles.Concat(GetProjectFiles(subDir));
			}

			return relevantFiles;
		}
	}
}