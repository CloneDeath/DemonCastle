using DemonCastle.Editor.Editors.Components.BaseEntity;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Item;

public partial class ItemEditor : BaseEntityEditor {
	public override Texture2D TabIcon => IconTextures.File.ItemIcon;

	public ItemEditor(ProjectResources resources, ProjectInfo project, ItemInfo item) : base(resources, project, item, item, new ItemDetails(item)) { }
}