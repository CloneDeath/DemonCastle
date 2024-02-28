using DemonCastle.Editor.Editors.Components.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Item;

public partial class ItemEditor : BaseEntityEditor {
	public ItemEditor(ProjectResources resources, ProjectInfo project, ItemInfo item) : base(resources, project, item,
		item, new ItemDetails(item)) {

		Tabs.AddTab("Events", new Label{Text = "Events"});
	}
}