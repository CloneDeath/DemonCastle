using System;
using Path = System.IO.Path;
using File = System.IO.File;

namespace DemonCastle.Navigation;

public class FileNavigator : DirectoryNavigator {
	public string FilePath { get; private set; }
	public string FileName => Path.GetFileName(FilePath);
	public string Extension => Path.GetExtension(FilePath);
	public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(FileName);

	public FileNavigator(string filePath)
		: base(Path.GetDirectoryName(filePath) ?? throw new NullReferenceException()) {
		FilePath = filePath;
	}

	public void DeleteFile() => File.Delete(FilePath);

	public void RenameFile(string newName) {
		var newPath = Path.GetFullPath(Path.Combine(Directory, newName));
		File.Move(FilePath, newPath);
		FilePath = newPath;
		Directory = Path.GetDirectoryName(FilePath) ?? throw new NullReferenceException();
	}

	public void MoveTo(DirectoryNavigator directory) {
		var newPath = Path.GetFullPath(Path.Combine(directory.Directory, FileName));
		File.Move(FilePath, newPath);
		FilePath = newPath;
		Directory = Path.GetDirectoryName(FilePath) ?? throw new NullReferenceException();
	}

	public string LoadContent() => File.ReadAllText(FilePath);
	public void SaveContent(string content) => File.WriteAllText(FilePath, content);
}