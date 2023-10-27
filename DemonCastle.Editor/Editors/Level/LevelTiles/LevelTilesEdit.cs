using DemonCastle.Editor.Editors.Level.LevelTiles.Tools;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.LevelTiles;

public partial class LevelTilesEdit : HSplitContainer {
	public LevelTilesEdit(LevelInfo level) {
		AddChild(new TileToolsPanel(level));
		AddChild(new LevelTilesView(level));
	}
}