using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Locations;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels;

public class LevelInfo : FileInfo<LevelFile>, IListableInfo {
	private readonly List<AreaInfo> _areas;

	public LevelInfo(FileNavigator<LevelFile> file) : base(file) {
		TileSet = new LevelTileSet(file.Resource, File);
		_areas = Resource.Areas.Select(area => new AreaInfo(file, area, this)).ToList();
	}

	public string ListLabel => Name;

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
		}
	}

	public LevelTileSet TileSet { get; }

	public Vector2I TileSize {
		get => new(Resource.TileWidth, Resource.TileHeight);
		set {
			Resource.TileWidth = value.X;
			Resource.TileHeight = value.Y;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2I AreaSize {
		get => new(Resource.AreaWidth, Resource.AreaHeight);
		set {
			Resource.AreaWidth = value.X;
			Resource.AreaHeight = value.Y;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid StartingPositionAreaId {
		get => Resource.StartingPosition.AreaId;
		set {
			Resource.StartingPosition.AreaId = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2I StartingPositionAreaCell {
		get => new(Resource.StartingPosition.X, Resource.StartingPosition.Y);
		set {
			Resource.StartingPosition.X = value.X;
			Resource.StartingPosition.Y = value.Y;
			Save();
			OnPropertyChanged();
		}
	}

	public IEnumerable<AreaInfo> Areas => _areas;

	public Vector2 StartingLocation => GetAreaById(Resource.StartingPosition.AreaId).TilePosition
									   + TileSize * new Vector2(Resource.StartingPosition.X, Resource.StartingPosition.Y)
									   + TileSize / new Vector2(1 / 2f, 1);

	public TileSize AreaScale => new(AreaSize, TileSize);

	public AreaInfo CreateArea() {
		var area = new AreaData();
		Resource.Areas.Add(area);
		var areaInfo = new AreaInfo(File, area, this);
		_areas.Add(areaInfo);
		OnPropertyChanged(nameof(Areas));
		return areaInfo;
	}

	private AreaInfo GetAreaById(Guid id) {
		return _areas.First(a => a.Id == id);
	}

	public AreaInfo? GetAreaAt(AreaPosition position) {
		return _areas.FirstOrDefault(a => a.Region.ContainsAreaIndex(position.AreaIndex));
	}

	public TileInfo GetTileInfo(Guid tileId) => TileSet.GetTileInfo(tileId);
}