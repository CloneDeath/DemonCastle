using DemonCastle.Editor.Editors.Level.Area;
using DemonCastle.Editor.Editors.Level.LevelOverview;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Level;

public partial class LevelEditor : Control {
	protected LevelInfo Level { get; }

	protected VSplitContainer SplitContainer { get; }
	protected LevelOverviewEdit LevelOverview { get; }
	protected Button ExpandCollapseButton { get; }
	protected AreaEdit Area { get; }

	public LevelEditor(ProjectResources resources, LevelInfo level) {
		Name = nameof(LevelEditor);
		CustomMinimumSize = new Vector2I(600, 400);

		Level = level;

		AddChild(SplitContainer = new VSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(LevelOverview = new LevelOverviewEdit(level));
		LevelOverview.AreaSelected += LevelOverview_OnAreaSelected;

		VBoxContainer bottom;
		SplitContainer.AddChild(bottom = new VBoxContainer());
		bottom.AddChild(ExpandCollapseButton = new Button {
			Text = "Collapse"
		});
		ExpandCollapseButton.Pressed += ExpandCollapseButton_OnPressed;

		bottom.AddChild(Area = new AreaEdit(resources, level) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		Area.AreaSelected += Area_OnAreaSelected;
	}

	private void ExpandCollapseButton_OnPressed() {
		SplitContainer.Collapsed = !SplitContainer.Collapsed;
		LevelOverview.Visible = !SplitContainer.Collapsed;
		ExpandCollapseButton.Text = SplitContainer.Collapsed ? "Expand" : "Collapse";
	}

	private void Area_OnAreaSelected(AreaInfo area) {
		LevelOverview.SelectArea(area);
	}

	private void LevelOverview_OnAreaSelected(AreaInfo area) {
		Area.SelectedArea = area;
	}
}