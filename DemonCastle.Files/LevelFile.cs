using System;
using System.Collections.Generic;
using Godot;

namespace DemonCastle.Files;

public class LevelFile : IGameFile {
	public int FileVersion => 3;
	public string Name { get; set; } = string.Empty;
	public Guid Id { get; set; } = Guid.NewGuid();
	public int TileWidth { get; set; } = 16;
	public int TileHeight { get; set; } = 16;
	public int AreaWidth { get; set; } = 16;
	public int AreaHeight { get; set; } = 9;
	public StartingData StartingPosition { get; set; } = new();
	public List<TileData> Tiles { get; set; } = new();
	public List<AreaData> Areas { get; set; } = new();
}

public class StartingData {
	public Guid AreaId { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
}

public class AreaData {
	public Guid Id = Guid.NewGuid();
	public string Name = string.Empty;
	public int X;
	public int Y;
	public int Width = 1;
	public int Height = 1;
	public List<MonsterData> Monsters { get; set; } = new();
	public List<TileMapLayerData> TileMapLayers { get; set; } = new();
}

public class MonsterData {
	public Guid MonsterId;
	public Vector2 Position;
}

public class TileMapLayerData {
	public string Name = "Layer";
	public int ZIndex;
	public List<TileMapData> TileMap { get; set; } = new();
}

public class TileMapData {
	public int X { get; set; }
	public int Y { get; set; }
	public Guid TileId { get; set; }
}