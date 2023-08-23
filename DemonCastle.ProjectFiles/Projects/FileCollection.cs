using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DemonCastle.ProjectFiles.Projects {
	public partial class FileCollection {
		private readonly string _directory;

		public FileCollection(string directory) {
			_directory = directory;
		}

		public IEnumerable<string> AllFiles => GetAllFiles(_directory);
		public IEnumerable<string> ProjectFiles => AllFiles.Where(file => file.EndsWith(".dcp"));

		protected IEnumerable<string> GetAllFiles(string directory) {
			if (!DirAccess.Exists(directory)) return new string[0];
			
			IEnumerable<string> files = DirAccess.GetFiles(directory);
			var subFiles = DirAccess.GetDirectories(directory).SelectMany(GetAllFiles);
			return files.Concat(subFiles);
		}

		public string GetFile(string file) {
			return Path3D.Combine(_directory, file);
		}
	}
}