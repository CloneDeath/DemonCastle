using System.IO;
using Newtonsoft.Json;

namespace DemonCastle.Projects.Resources {
	public class FileNavigator<T> : DirectoryNavigator {
		public T Resource { get; }
		
		public FileNavigator(string filePath) : this(filePath, new ProjectResources()) { }
		public FileNavigator(string filePath, ProjectResources resources) 
			: base(Path.GetDirectoryName(filePath), resources) {
			
			var fileContents = File.ReadAllText(filePath);
			Resource = JsonConvert.DeserializeObject<T>(fileContents);
		}
	}
}