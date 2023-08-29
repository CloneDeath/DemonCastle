using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data; 

public class FileInfo<TFile> {
	protected FileNavigator<TFile> File { get; }
		
	protected TFile Resource => File.Resource;
	public string FileName => File.FileName;
	public string Directory => File.Directory;

	public FileInfo(FileNavigator<TFile> file) {
		File = file;
	}
		
	public void Save() => File.Save();
}