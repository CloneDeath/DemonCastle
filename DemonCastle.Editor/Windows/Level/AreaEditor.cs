using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level; 

public partial class AreaEditor : ScrollContainer {
	private void LoadLevel(LevelInfo levelInfo) {
		foreach (var area in levelInfo.Areas) {
			Root.AddChild(new AreaCell(area));
		}
	}
}