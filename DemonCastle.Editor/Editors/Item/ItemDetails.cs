using DemonCastle.Editor.Editors.BaseEntity;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Item;

public partial class ItemDetails : BaseEntityDetails<ItemInfo, ItemFile> {
	public ItemDetails(ItemInfo entity) : base(entity) { }
}