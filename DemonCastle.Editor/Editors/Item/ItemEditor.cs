using DemonCastle.Editor.Editors.BaseEntity;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Item;

public partial class ItemEditor : BaseEntityEditor<ItemInfo, ItemFile> {
	public override Texture2D TabIcon => IconTextures.ItemIcon;

	public ItemEditor(ItemInfo item) : base(item, new ItemDetails(item)) { }
}