using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level; 

public partial class AreaEditor : ScrollContainer {
	public void Reload() => LoadLevel(_levelInfo);

	private void LoadLevel(LevelInfo levelInfo) {
		foreach (var child in Root.GetChildren()) {
			child.QueueFree();
		}
		foreach (var area in levelInfo.Areas) {
			Root.AddChild(new AreaCell(area));
		}
	}
}