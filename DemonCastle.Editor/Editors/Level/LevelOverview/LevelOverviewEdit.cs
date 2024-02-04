using System;
using DemonCastle.Editor.Editors.Level.LevelOverview.Minimap;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using Godot;

namespace DemonCastle.Editor.Editors.Level.LevelOverview;

public partial class LevelOverviewEdit : HSplitContainer {
	protected LevelDetailsPanel DetailsPanel { get; }
	protected MinimapView Minimap { get; }

	public event Action<AreaInfo>? AreaSelected;

	public LevelOverviewEdit(LevelInfo level) {
		Name = nameof(LevelOverviewEdit);

		AddChild(DetailsPanel = new LevelDetailsPanel(level));
		AddChild(Minimap = new MinimapView(level));
		Minimap.AreaSelected += Minimap_OnAreaSelected;
	}

	public void SelectArea(AreaInfo area) {
		Minimap.SelectArea(area);
	}

	private void Minimap_OnAreaSelected(AreaInfo area) {
		AreaSelected?.Invoke(area);
	}
}