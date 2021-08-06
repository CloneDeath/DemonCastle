using System.IO;
using Newtonsoft.Json;

namespace DemonCastle.Projects.Data {
	public abstract class ResourceInfo<T> {
		protected string FilePath { get; }
		protected T Resource { get; }

		protected ResourceInfo(string filePath) {
			FilePath = filePath;
			var fileContents = File.ReadAllText(filePath);
			Resource = JsonConvert.DeserializeObject<T>(fileContents);
		}
		
		protected string LocalDirectory => Path.GetDirectoryName(FilePath);
		protected FileCollection Files => new FileCollection(LocalDirectory);
	}
}