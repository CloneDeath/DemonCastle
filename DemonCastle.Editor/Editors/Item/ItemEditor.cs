using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Item;

public partial class ItemEditor : Components.BaseEntity.BaseEntityEditor<ItemInfo, ItemFile> {
	public override Texture2D TabIcon => IconTextures.ItemIcon;

	public ItemEditor(ItemInfo item) : base(item, new ItemDetails(item)) { }
}