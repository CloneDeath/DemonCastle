using DemonCastle.Editor.Editors.Components.BaseEntity;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Item;

public partial class ItemEditor : BaseEntityEditor<ItemInfo, ItemFile> {
	public override Texture2D TabIcon => IconTextures.File.ItemIcon;

	public ItemEditor(ProjectInfo project, ItemInfo item) : base(project, item, item, new ItemDetails(item)) { }
}