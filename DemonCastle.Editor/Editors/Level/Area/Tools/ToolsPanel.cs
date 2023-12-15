using DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools;
using DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools;

public partial class ToolsPanel : TabContainer {
	private readonly TileToolsPanel _tileToolsPanel;
	private readonly MonsterToolsPanel _monsterToolsPanel;

	public AreaInfo? Area {
		get => _monsterToolsPanel.Area;
		set => _monsterToolsPanel.Area = value;
	}

	public ToolsPanel(ProjectInfo project, LevelInfo level) {
		Name = nameof(ToolsPanel);

		AddChild(_tileToolsPanel = new TileToolsPanel(level));
		SetTabTitle(0, "Tiles");
		AddChild(_monsterToolsPanel = new MonsterToolsPanel(project));
		_monsterToolsPanel.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
		SetTabTitle(1, "Monsters");
	}

	public TileInfo? SelectedTile => _tileToolsPanel.SelectedTile;
}