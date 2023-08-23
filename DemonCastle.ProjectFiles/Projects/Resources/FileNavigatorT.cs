using System.IO;
using Newtonsoft.Json;

namespace DemonCastle.ProjectFiles.Projects.Resources {
	public partial class FileNavigator<T> : FileNavigator {
		public T Resource { get; }
		
		public FileNavigator(string filePath) : this(filePath, new ProjectResources()) { }
		public FileNavigator(string filePath, ProjectResources resources) 
			: base(filePath, resources) {
			var fileContents = File.ReadAllText(filePath);
			Resource = JsonConvert.DeserializeObject<T>(fileContents);
		}

		public void Save() {
			var contents = JsonConvert.SerializeObject(Resource, Formatting.Indented);
			File.WriteAllText(FilePath, contents);
		}
	}
}