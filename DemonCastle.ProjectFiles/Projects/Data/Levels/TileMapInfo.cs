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

	protected TileInfo Tile => AreaInfo.GetTileInfo(TileName);
		
	public Vector2I AreaPosition => new Vector2I(TileMapData.X, TileMapData.Y) * AreaInfo.TileSize;
	public Vector2I Position => AreaPosition + AreaInfo.TilePosition;
	public Vector2I TileSize => AreaInfo.TileSize;
	public string TileName => TileMapData.Tile;
	public Texture2D Texture => Tile.Texture;
	public Rect2 Region => Tile.Region;
}