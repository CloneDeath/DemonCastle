using System;
using DemonCastle.Editor.Editors.Level.LevelOverview.Minimap;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.LevelOverview;

public partial class LevelOverviewEdit : HSplitContainer {
	protected LevelDetailsPanel DetailsPanel { get; }
	protected MinimapView Minimap { get; }

	public event Action<AreaInfo>? AreaSelected;

	public LevelOverviewEdit(LevelInfo level) {
		Name = nameof(LevelOverviewEdit);

		AddChild(DetailsPanel = new LevelDetailsPanel(level));
		AddChild(Minimap = new MinimapView(level) {
			CustomMinimumSize = new Vector2(0, 150)
		});
		Minimap.AreaSelected += Minimap_OnAreaSelected;
	}

	public void SelectArea(AreaInfo area) {
		Minimap.SelectArea(area);
	}

	private void Minimap_OnAreaSelected(AreaInfo area) {
		AreaSelected?.Invoke(area);
	}
}