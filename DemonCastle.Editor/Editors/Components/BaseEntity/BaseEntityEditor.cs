using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Components.BaseEntity;

public abstract partial class BaseEntityEditor : BaseEditor {
	protected HSplitContainer SplitContainer { get; }
	protected BaseEntityTabContainer Tabs { get; }

	protected BaseEntityEditor(ProjectResources resources, ProjectInfo project, IFileInfo file, IBaseEntityInfo entity, Control detailsPanel) {
		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(detailsPanel);
		detailsPanel.CustomMinimumSize = new Vector2(300, 300);
		SplitContainer.AddChild(Tabs = new BaseEntityTabContainer(resources, project, file));
		Tabs.Load(entity);
	}
}