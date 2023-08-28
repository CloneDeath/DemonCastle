using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels; 

public class LevelInfo : IListableInfo {
	protected FileNavigator<LevelFile> File { get; }
	protected LevelFile Level => File.Resource;
	public string FileName => File.FileName;

	public LevelTileSet TileSet { get; }

	public LevelInfo(FileNavigator<LevelFile> file) {
		File = file;
		TileSet = new LevelTileSet(Level, File);
	}
		
	public string Name {
		get => Level.Name;
		set => Level.Name = value;
	}

	public int TileWidth {
		get => Level.TileWidth;
		set => Level.TileWidth = value;
	}

	public int TileHeight {
		get => Level.TileHeight;
		set => Level.TileHeight = value;
	}

	public int AreaWidth {
		get => Level.AreaWidth;
		set => Level.AreaWidth = value;
	}

	public int AreaHeight {
		get => Level.AreaHeight;
		set => Level.AreaHeight = value;
	}

	public Vector2I TileSize => new(Level.TileWidth, Level.TileHeight);
	public Vector2I AreaSize => new(Level.AreaWidth, Level.AreaHeight);

	public IEnumerable<AreaInfo> Areas => Level.Areas.Select(area => new AreaInfo(area, this));

	private AreaInfo GetAreaByName(string name) {
		var area = Level.Areas.First(a => a.Name == name);
		return new AreaInfo(area, this);
	}

	public Vector2 StartingLocation => TileSize * (
													  GetAreaByName(Level.StartingPosition.Area).TilePosition
													  + new Vector2(Level.StartingPosition.X, Level.StartingPosition.Y)
												  ) + new Vector2(TileWidth/2f, TileHeight);
}