using System;
using System.Collections.Generic;
using Godot;

namespace DemonCastle.Files;

public class LevelFile : IGameFile {
	public int FileVersion => 1;
	public string Name { get; set; } = string.Empty;
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

public class TileData {
	public string Name { get; set; } = string.Empty;
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Source { get; set; } = string.Empty;
	public Guid SpriteId { get; set; }
	public List<Vector2> Collision { get; set; } = new();
	public int Width { get; set; } = 1;
	public int Height { get; set; } = 1;
	public StairData? Stairs { get; set; }
}

public class StairData {
	public Vector2 Start { get; set; }
	public Vector2 End { get; set; }
}

public class AreaData {
	public Guid Id = Guid.NewGuid();
	public string Name = string.Empty;
	public int X;
	public int Y;
	public int Width = 1;
	public int Height = 1;
	public List<MonsterData> Monsters { get; set; } = new();
	public List<TileMapData> TileMap { get; set; } = new();
}

public class MonsterData {
	public Guid MonsterId;
	public Vector2 Position;
}

public class TileMapData {
	public int X { get; set; }
	public int Y { get; set; }
	public Guid TileId { get; set; }
}