using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.LevelTiles;

public partial class TilesView : Node2D{
	private readonly AreaInfo _areaInfo;

	public TilesView(AreaInfo areaInfo) {
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