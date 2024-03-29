using System;
using DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools;
using DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools;

public partial class ToolsPanel : TabContainer {
	private readonly TileToolsPanel _tileToolsPanel;
	private readonly MonsterToolsPanel _monsterToolsPanel;
	public event Action<int>? SelectedLayerIndexChanged;

	public AreaInfo? Area {
		get => _monsterToolsPanel.Area;
		set {
			_tileToolsPanel.LoadArea(value);
			_monsterToolsPanel.Area = value;
		}
	}

	public TileTool SelectedTool => _tileToolsPanel.SelectedTool;

	public ToolsPanel(ProjectResources resources, LevelInfo level) {
		Name = nameof(ToolsPanel);

		AddChild(_tileToolsPanel = new TileToolsPanel(resources, level));
		_tileToolsPanel.SelectedLayerIndexChanged += index => SelectedLayerIndexChanged?.Invoke(index);
		SetTabTitle(0, "Tiles");
		AddChild(_monsterToolsPanel = new MonsterToolsPanel(resources));
		SetTabTitle(1, "Monsters");
	}

	public TileInfo? SelectedTile => _tileToolsPanel.SelectedTile;
	public TileMapLayerInfo? SelectedLayer => _tileToolsPanel.SelectedLayer;
}