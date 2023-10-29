using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels;

public class LevelTileSet {
	protected LevelFile Level { get; }
	protected FileNavigator<LevelFile> File { get; }

	public IEnumerable<TileInfo> Tiles => Level.Tiles.Select(t => new TileInfo(File, t));

	public LevelTileSet(LevelFile level, FileNavigator<LevelFile> file) {
		File = file;
		Level = level;
	}

	public TileInfo GetTileInfo(Guid tileId) {
		return Tiles.First(t => t.Id == tileId);
	}

	public TileInfo CreateTile() {
		var tileData = new TileData();
		Level.Tiles.Add(tileData);
		File.Save();
		return new TileInfo(File, tileData);
	}

	public void DeleteTile(TileInfo tileInfo) {
		var index = Level.Tiles.FindIndex(t => t.Name == tileInfo.Name);
		if (index < 0) return;
		Level.Tiles.RemoveAt(index);
	}
}