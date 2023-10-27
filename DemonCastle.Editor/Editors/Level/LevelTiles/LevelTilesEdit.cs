using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.LevelTiles;

public partial class LevelTilesEdit : HSplitContainer {
	public LevelTilesEdit(LevelInfo level) {
		AddChild(new LevelTilesView(level));
		AddChild(new LevelTilesView(level));
	}
}