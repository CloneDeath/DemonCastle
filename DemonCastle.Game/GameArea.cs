using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GameArea : Node2D {
	public GameArea(AreaInfo area) {
		Name = nameof(GameArea);

		foreach (var tileMapInfo in area.TileMap) {
			var tileInfo = tileMapInfo.Tile;
			AddChild(new GameTile(tileInfo) {
				Position = tileMapInfo.Position.ToPixelPositionInArea()
			});
		}
	}
}