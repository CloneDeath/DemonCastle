using System.IO;
using DemonCastle.ProjectFiles;
using Newtonsoft.Json;

namespace DemonCastle.Projects {
	public class ProjectInfo {
		private readonly ProjectFile _projectFile;
		private readonly string _filePath;

		public ProjectInfo(string filePath) {
			_filePath = filePath;
			var fileContents = File.ReadAllText(filePath);
			_projectFile = JsonConvert.DeserializeObject<ProjectFile>(fileContents);
		}

		public string Name => _projectFile.Name;
	}
}