using System;
using DemonCastle.Editor.Editors.Level.Area.Details;
using DemonCastle.Editor.Editors.Level.Area.Tools;
using DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;
using DemonCastle.Editor.Editors.Level.Area.View;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area;

public partial class AreaEdit : HSplitContainer {
	protected VBoxContainer LeftPanel { get; }
	protected AreaDetails Details { get; }
	protected ToolsPanel Tools { get; }

	protected LevelAreasView RightPanel { get; }

	public event Action<AreaInfo>? AreaSelected;

	public AreaEdit(ProjectResources resources, LevelInfo level) {
		Name = nameof(AreaEdit);

		AddChild(LeftPanel = new VBoxContainer {
			CustomMinimumSize = new Vector2(275, 0)
		});
		LeftPanel.AddChild(Details = new AreaDetails());
		LeftPanel.AddChild(Tools = new ToolsPanel(resources, level) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		AddChild(RightPanel = new LevelAreasView(resources, level));
		RightPanel.AreaSelected += LevelAreasView_OnAreaSelected;
		RightPanel.AreaTileSelected += LevelAreasView_OnAreaTileSelected;
		RightPanel.AreaTileCleared += LevelAreasView_OnAreaTileCleared;

		Tools.SelectedLayerIndexChanged += index => RightPanel.SelectedLayerIndex = index;
	}

	public AreaInfo? SelectedArea {
		get => Details.Area;
		set {
			Details.Area = value;
			Tools.Area = value;
			if (value == null) {
				RightPanel.DeselectAllAreas();
			}
			else {
				RightPanel.SelectArea(value);
			}
		}
	}

	private void LevelAreasView_OnAreaSelected(AreaInfo area) {
		Details.Area = area;
		Tools.Area = area;
		AreaSelected?.Invoke(area);
	}

	private void LevelAreasView_OnAreaTileSelected(AreaInfo area, Vector2I cell) {
		var selectedTile = Tools.SelectedTile;
		if (selectedTile == null) return;
		var selectedLayer = Tools.SelectedLayer;
		if (selectedLayer == null) return;

		switch (Tools.SelectedTool) {
			case TileTool.Brush:
				area.SetTile(cell, selectedLayer.ZIndex, selectedTile.Id);
				break;
			case TileTool.Fill:
				area.FloodFillTile(cell, selectedLayer.ZIndex, selectedTile.Id);
				return;
			default:
				throw new NotSupportedException();
		}
	}

	private void LevelAreasView_OnAreaTileCleared(AreaInfo area, Vector2I cell) {
		var selectedLayer = Tools.SelectedLayer;
		if (selectedLayer == null) return;

		area.ClearTile(cell, selectedLayer.ZIndex);
	}
}