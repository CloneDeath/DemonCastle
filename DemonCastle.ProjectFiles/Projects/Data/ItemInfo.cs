using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class ItemInfo : BaseEntityInfo<ItemFile>, IFileInfo {
	public ItemInfo(FileNavigator<ItemFile> file) : base(file, file.Resource) {
	}

	public string FileName => File.FileName;
	public string Directory => File.Directory;
	void IFileInfo.Save() => base.Save();
}