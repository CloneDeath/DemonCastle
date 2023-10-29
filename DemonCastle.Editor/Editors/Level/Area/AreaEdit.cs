using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;
using TileToolsPanel = DemonCastle.Editor.Editors.Level.Area.TileTools.TileToolsPanel;

namespace DemonCastle.Editor.Editors.Level.Area;

public partial class AreaEdit : HSplitContainer {
	protected VBoxContainer LeftPanel { get; }
	public AreaEdit(LevelInfo level) {
		AddChild(LeftPanel = new VBoxContainer());
		LeftPanel.AddChild(new TileToolsPanel(level));
		AddChild(new AreaTiles.LevelTilesView(level));
	}
}