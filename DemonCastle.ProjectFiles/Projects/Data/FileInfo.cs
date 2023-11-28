using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public interface IFileInfo {
	public string FileName { get; }
	public string Directory { get; }
	public void Save();
}

public class FileInfo<TFile> : IFileInfo {
	protected FileNavigator<TFile> File { get; }

	protected TFile Resource => File.Resource;
	public string FileName => File.FileName;
	public string Directory => File.Directory;

	public FileInfo(FileNavigator<TFile> file) {
		File = file;
	}

	public void Save() => File.Save();
}