using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class AreaTilesView : Control {
	private readonly AreaInfo _areaInfo;

	public AreaTilesView(AreaInfo areaInfo) {
		_areaInfo = areaInfo;
		ReloadArea();
	}

	private void ReloadArea() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		foreach (var tileMapInfo in _areaInfo.TileMap) {
			AddChild(new TileView(tileMapInfo));
		}
	}
}