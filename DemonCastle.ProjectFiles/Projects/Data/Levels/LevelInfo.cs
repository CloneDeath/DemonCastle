using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels; 

public class LevelInfo : FileInfo<LevelFile>, IListableInfo {
	public LevelTileSet TileSet { get; }

	public LevelInfo(FileNavigator<LevelFile> file) : base(file) {
		TileSet = new LevelTileSet(file.Resource, File);
	}

	public string Name {
		get => Resource.Name;
		set { Resource.Name = value; Save(); }
	}

	public int TileWidth {
		get => Resource.TileWidth;
		set { Resource.TileWidth = value; Save(); }
	}

	public int TileHeight {
		get => Resource.TileHeight;
		set { Resource.TileHeight = value; Save(); }
	}

	public int AreaWidth {
		get => Resource.AreaWidth;
		set { Resource.AreaWidth = value; Save(); }
	}

	public int AreaHeight {
		get => Resource.AreaHeight;
		set { Resource.AreaHeight = value; Save(); }
	}

	public Vector2I TileSize => new(Resource.TileWidth, Resource.TileHeight);
	public Vector2I AreaSize => new(Resource.AreaWidth, Resource.AreaHeight);

	public IEnumerable<AreaInfo> Areas => Resource.Areas.Select(area => new AreaInfo(area, this));

	private AreaInfo GetAreaByName(string name) {
		var area = Resource.Areas.First(a => a.Name == name);
		return new AreaInfo(area, this);
	}

	public Vector2 StartingLocation => TileSize * (
													  GetAreaByName(Resource.StartingPosition.Area).TilePosition
													  + new Vector2(Resource.StartingPosition.X, Resource.StartingPosition.Y)
												  ) + new Vector2(TileWidth/2f, TileHeight);
}