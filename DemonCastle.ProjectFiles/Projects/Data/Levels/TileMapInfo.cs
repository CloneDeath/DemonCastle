using System;
using DemonCastle.ProjectFiles.Locations;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels;

public class TileMapInfo {
	protected TileMapData TileMapData { get; }
	protected AreaInfo AreaInfo { get; }

	public TileMapInfo(TileMapData tileMapData, AreaInfo areaInfo) {
		TileMapData = tileMapData;
		AreaInfo = areaInfo;
	}

	protected LevelTileSet TileSet => AreaInfo.LevelTileSet;
	protected Vector2I TileIndex => new(TileMapData.X, TileMapData.Y);

	public TileInfo Tile => AreaInfo.GetTileInfo(TileId);

	public Vector2I TileScale => AreaInfo.TileSize;
	public TilePosition Position => new(TileIndex, AreaInfo.PositionOfArea, TileScale);
	public Guid TileId => TileMapData.TileId;
	public Texture2D Texture => Tile.Texture;
	public Rect2 Region => Tile.Region;
	public bool FlipHorizontal => Tile.FlipHorizontal;
	public Vector2I Span => Tile.Span;
}