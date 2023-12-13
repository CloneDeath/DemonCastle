using DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools;
using DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools;

public partial class ToolsPanel : TabContainer {
	private readonly TileToolsPanel _tileToolsPanel;

	public ToolsPanel(LevelInfo level) {
		Name = nameof(ToolsPanel);

		AddChild(_tileToolsPanel = new TileToolsPanel(level));
		SetTabTitle(0, "Tiles");
		AddChild(new MonsterToolsPanel());
		SetTabTitle(1, "Monsters");
	}

	public TileInfo? SelectedTile => _tileToolsPanel.SelectedTile;
}