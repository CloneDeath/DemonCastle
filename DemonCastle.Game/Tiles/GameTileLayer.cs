using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.Tiles;

public partial class GameTileLayer : Node2D {
	public GameTileLayer(IGameState game, LevelInfo level, TileMapLayerInfo layer, IGameLogger logger, DebugState debug) {
		Name = $"{nameof(GameTileLayer)}@{layer.ZIndex} ({layer.Name})";

		ZIndex = layer.ZIndex;
		foreach (var tileMapInfo in layer.TileMap) {
			var tileInfo = tileMapInfo.Tile;
			AddChild(new GameTile(game, level, tileInfo, logger, debug) {
				Name = $"{nameof(GameTile)}@{tileMapInfo.Position.ToTileIndex()}",
				Position = tileMapInfo.Position.ToPixelPositionInArea()
			});
		}
	}
}