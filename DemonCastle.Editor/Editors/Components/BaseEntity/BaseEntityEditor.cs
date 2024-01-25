using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components.BaseEntity;

public abstract partial class BaseEntityEditor : BaseEditor {
	public override string TabText { get; }
	protected HSplitContainer SplitContainer { get; }
	protected BaseEntityTabContainer Tabs { get; }

	protected BaseEntityEditor(ProjectInfo project, IFileInfo file, IBaseEntityInfo entity, Control detailsPanel) {
		TabText = file.FileName;

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(detailsPanel);
		detailsPanel.CustomMinimumSize = new Vector2(300, 300);
		SplitContainer.AddChild(Tabs = new BaseEntityTabContainer(project, file));
		Tabs.Load(entity);
	}
}