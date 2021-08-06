using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DemonCastle.Projects {
	public class FileCollection {
		private readonly string _directory;

		public FileCollection(string directory) {
			_directory = directory;
		}

		public IEnumerable<string> AllFiles => GetAllFiles(_directory);
		public IEnumerable<string> ProjectFiles => AllFiles.Where(file => file.EndsWith(".dcp"));

		protected IEnumerable<string> GetAllFiles(string directory) {
			if (!Directory.Exists(directory)) return new string[0];
			
			IEnumerable<string> files = Directory.GetFiles(directory);
			var subFiles = Directory.GetDirectories(directory).SelectMany(GetAllFiles);
			return files.Concat(subFiles);
		}

		public string GetFile(string file) {
			return Path.Combine(_directory, file);
		}
	}
}