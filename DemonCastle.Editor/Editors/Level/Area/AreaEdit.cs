using DemonCastle.Editor.Editors.Level.Area.Tools;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area;

public partial class AreaEdit : HSplitContainer {
	public AreaEdit(LevelInfo level) {
		AddChild(new TileToolsPanel(level));
		AddChild(new LevelTilesView(level));
	}
}