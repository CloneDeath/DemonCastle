using DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools;
using DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools;

public partial class ToolsPanel : TabContainer {
	private readonly TileToolsPanel _tileToolsPanel;
	private readonly MonsterToolsPanel _monsterToolsPanel;

	public AreaInfo? Area {
		get => _monsterToolsPanel.Area;
		set {
			_tileToolsPanel.LoadArea(value);
			_monsterToolsPanel.Area = value;
		}
	}

	public ToolsPanel(ProjectInfo project, LevelInfo level) {
		Name = nameof(ToolsPanel);

		AddChild(_tileToolsPanel = new TileToolsPanel(project, level));
		SetTabTitle(0, "Tiles");
		AddChild(_monsterToolsPanel = new MonsterToolsPanel(project));
		SetTabTitle(1, "Monsters");
	}

	public TileInfo? SelectedTile => _tileToolsPanel.SelectedTile;
	public TileMapLayerInfo? SelectedLayer => _tileToolsPanel.SelectedLayer;
}