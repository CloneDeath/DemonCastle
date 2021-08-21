using System.IO;
using Newtonsoft.Json;

namespace DemonCastle.ProjectFiles.Projects.Resources {
	public class FileNavigator<T> : DirectoryNavigator {
		public string FilePath { get; }
		public T Resource { get; }
		
		public FileNavigator(string filePath) : this(filePath, new ProjectResources()) { }
		public FileNavigator(string filePath, ProjectResources resources) 
			: base(Path.GetDirectoryName(filePath), resources) {
			FilePath = filePath;

			var fileContents = File.ReadAllText(filePath);
			Resource = JsonConvert.DeserializeObject<T>(fileContents);
		}
	}
}