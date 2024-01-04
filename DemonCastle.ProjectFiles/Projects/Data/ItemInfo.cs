using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class ItemInfo : BaseEntityInfo<ItemFile> {
	public ItemInfo(FileNavigator<ItemFile> file) : base(file) {
	}
}