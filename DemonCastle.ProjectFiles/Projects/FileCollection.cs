using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DemonCastle.ProjectFiles.Projects;

public class FileCollection {
	private readonly string _directory;

	public FileCollection(string directory) {
		_directory = directory;
	}

	public IEnumerable<string> AllFiles => GetAllFiles(_directory);
	public IEnumerable<string> ProjectFiles => AllFiles.Where(file => file.EndsWith(FileType.Project.Extension));

	protected IEnumerable<string> GetAllFiles(string directory) {
		if (!Directory.Exists(directory)) return Array.Empty<string>();

		IEnumerable<string> files = Directory.GetFiles(directory);
		var subFiles = Directory.GetDirectories(directory).SelectMany(GetAllFiles);
		return files.Concat(subFiles);
	}
}