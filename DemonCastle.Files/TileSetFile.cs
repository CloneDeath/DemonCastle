using System;
using System.Collections.Generic;
using DemonCastle.Files.BaseEntity;
using DemonCastle.Files.Common;
using Godot;

namespace DemonCastle.Files;

public class TileSetFile : IGameFile {
	public int FileVersion => 1;
	public string Name = "Tile Set";
	public Guid Id = Guid.NewGuid();

	public List<TileData> Tiles { get; set; } = new();
}

public class TileData : BaseEntityFile {
	public TileData() {
		Name = "Tile";
		Size = new Size(1, 1);
	}

	public List<Vector2> Collision = new();
	public StairData? Stairs;
}

public class StairData {
	public Vector2 Start { get; set; }
	public Vector2 End { get; set; }
}