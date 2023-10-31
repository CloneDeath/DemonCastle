using DemonCastle.Editor.Editors.Level.Area.AreaTiles;
using DemonCastle.Editor.Editors.Level.Area.Details;
using DemonCastle.Editor.Editors.Level.Area.TileTools;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area;

public partial class AreaEdit : HSplitContainer {
	protected VBoxContainer LeftPanel { get; }
	protected AreaDetails Details { get; }
	protected TileToolsPanel Tools { get; }

	protected LevelAreasView RightPanel { get; }

	public AreaEdit(LevelInfo level) {
		Name = nameof(AreaEdit);

		AddChild(LeftPanel = new VBoxContainer());
		LeftPanel.AddChild(Details = new AreaDetails());
		LeftPanel.AddChild(Tools = new TileToolsPanel(level));
		AddChild(RightPanel = new LevelAreasView(level));
		RightPanel.AreaTileSelected += LevelAreasView_OnAreaTileSelected;
		RightPanel.AreaTileCleared += LevelAreasView_OnAreaTileCleared;
	}

	public AreaInfo? SelectedArea {
		get => Details.Proxy;
		set {
			Details.Proxy = value;
			if (value == null) {
				RightPanel.DeselectAllAreas();
			}
			else {
				RightPanel.SelectArea(value);
			}
		}
	}

	private void LevelAreasView_OnAreaTileSelected(AreaInfo area, Vector2I cell) {
		var selectedTile = Tools.SelectedTile;
		if (selectedTile == null) return;

		area.SetTile(cell, selectedTile.Id);
	}

	private void LevelAreasView_OnAreaTileCleared(AreaInfo area, Vector2I cell) {
		area.ClearTile(cell);
	}
}