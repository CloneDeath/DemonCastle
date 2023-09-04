using System.Collections.Generic;
using System.Linq;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels; 

public class AreaInfo {
	protected AreaData Area { get; }
	public LevelInfo LevelInfo { get; }

	public AreaInfo(AreaData area, LevelInfo levelInfo) {
		Area = area;
		LevelInfo = levelInfo;
	}

	public string Name {
		get => Area.Name;
		set { Area.Name = value; LevelInfo.Save(); }
	}

	public LevelTileSet LevelTileSet => LevelInfo.TileSet;
	public IEnumerable<TileMapInfo> TileMap => Area.TileMap.Select(tm => new TileMapInfo(tm, this));
	
	public Vector2I AreaPosition {
		get => new(Area.X, Area.Y);
		set {
			Area.X = value.X;
			Area.Y = value.Y;
			LevelInfo.Save();
		}
	}

	public Vector2I Size {
		get => new(Area.Width, Area.Height);
		set {
			Area.Width = value.X;
			Area.Height = value.Y;
			LevelInfo.Save();
		}
	}

	public Vector2I TilePosition => LevelInfo.TileSize * AreaPosition * LevelInfo.AreaSize;
	
	public Vector2I TileSize => LevelInfo.TileSize;
	public Vector2I AreaSize => LevelInfo.AreaSize;

	public TileInfo GetTileInfo(string tileName) => LevelInfo.GetTileInfo(tileName);
	public LevelTileSet TileSet => LevelInfo.TileSet;
}