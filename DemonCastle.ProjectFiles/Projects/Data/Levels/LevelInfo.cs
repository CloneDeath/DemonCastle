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

	public Vector2I TileSize {
		get => new (Resource.TileWidth, Resource.TileHeight);
		set {
			Resource.TileWidth = value.X;
			Resource.TileHeight = value.Y;
			Save();
		}
	}

	public Vector2I AreaSize {
		get => new(Resource.AreaWidth, Resource.AreaHeight);
		set {
			Resource.AreaWidth = value.X;
			Resource.AreaHeight = value.Y;
			Save();
		}
	}

	public IEnumerable<AreaInfo> Areas => Resource.Areas.Select(area => new AreaInfo(area, this));

	private AreaInfo GetAreaByName(string name) {
		var area = Resource.Areas.First(a => a.Name == name);
		return new AreaInfo(area, this);
	}

	public Vector2 StartingLocation => TileSize * (
													  GetAreaByName(Resource.StartingPosition.Area).TilePosition
													  + new Vector2(Resource.StartingPosition.X, Resource.StartingPosition.Y)
												  ) + TileSize / new Vector2(1/2f, 1);
}