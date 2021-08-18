using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DemonCastle.Projects {
	public class LocalProjectList {
		protected string GodotPath => "user://ProjectList.json";
		protected string GlobalPath => Godot.ProjectSettings.GlobalizePath(GodotPath);

		protected ProjectListFile GetProjectList() {
			if (!File.Exists(GlobalPath)) return new ProjectListFile();

			var contents = File.ReadAllText(GlobalPath);
			return JsonConvert.DeserializeObject<ProjectListFile>(contents);
		}
		
		public IEnumerable<string> ProjectFiles => GetProjectList().Projects;

		public void AddProject(string filePath) {
			var project = GetProjectList();
			project.Projects.Add(filePath);
			SaveProjectList(project);
		}

		private void SaveProjectList(ProjectListFile project) {
			var content = JsonConvert.SerializeObject(project);
			File.WriteAllText(GlobalPath, content);
		}
	}
}