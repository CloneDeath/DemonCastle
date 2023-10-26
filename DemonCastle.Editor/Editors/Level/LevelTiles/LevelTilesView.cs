using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.LevelTiles;

public partial class LevelTilesView : Panel {
	private readonly LevelInfo _levelInfo;

	public LevelTilesView(LevelInfo levelInfo) {
		_levelInfo = levelInfo;

		ReloadAreas();
	}

	private void ReloadAreas() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		foreach (var area in _levelInfo.Areas) {
			AddChild(new AreaTilesView(area) {
				Position = area.AreaPosition * area.AreaSize * area.TileSize
			});
		}
	}
}