using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game; 

public partial class GameLevel : Node2D {
	public Vector2 StartingLocation => Level.StartingLocation;

	protected void LoadLevel() {
		foreach (var area in Level.Areas) {
			LoadArea(area);
		}
	}

	protected void LoadArea(AreaInfo area) {
		foreach (var tileMapInfo in area.TileMap) {
			var tileInfo = Level.TileSet.GetTileInfo(tileMapInfo.TileName);
			AddChild(new GameTile(tileInfo, tileMapInfo));
		}
	}
}