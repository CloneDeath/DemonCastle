using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Locations;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels;

public class AreaInfo : BaseInfo<AreaData> {
	public LevelInfo Level { get; }
	private readonly List<TileMapInfo> _tileMapInfos = new();

	public AreaInfo(IFileNavigator file, AreaData area, LevelInfo level) : base(file, area) {
		Level = level;
		_tileMapInfos.AddRange(area.TileMap.Select(tm => new TileMapInfo(tm, this)));
		Monsters = new MonsterDataInfoCollection(file, this, area.Monsters);
	}

	public Guid Id => Data.Id;

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public LevelTileSet LevelTileSet => Level.TileSet;
	public IEnumerable<TileMapInfo> TileMap => _tileMapInfos;

	public MonsterDataInfoCollection Monsters { get; }

	public Vector2I AreaPosition {
		get => new(Data.X, Data.Y);
		set {
			Data.X = value.X;
			Data.Y = value.Y;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2I Size {
		get => new(Data.Width, Data.Height);
		set {
			Data.Width = value.X;
			Data.Height = value.Y;
			Save();
			OnPropertyChanged();
		}
	}

	public AreaPosition PositionOfArea => new(AreaPosition, Level.AreaSize, Level.TileSize);
	public AreaSize SizeOfArea => new(Size, Level.AreaSize, Level.TileSize);
	public AreaRegion Region => new(PositionOfArea, SizeOfArea);

	public Vector2I TilePosition => AreaPosition * Level.TileSize * Level.AreaSize;

	public Vector2I TileSize => Level.TileSize;
	public Vector2I AreaSize => Level.AreaSize;
	public LevelTileSet TileSet => Level.TileSet;

	public TileInfo GetTileInfo(Guid tileId) => Level.GetTileInfo(tileId);

	public void SetTile(Vector2I tileIndex, Guid tileId) {
		var info = _tileMapInfos.Find(info => info.Contains(tileIndex));
		if (info != null) {
			info.TileId = tileId;
		}
		else {
			var tileMapData = new TileMapData {
				X = tileIndex.X,
				Y = tileIndex.Y,
				TileId = tileId
			};
			Data.TileMap.Add(tileMapData);
			_tileMapInfos.Add(new TileMapInfo(tileMapData, this));
		}

		Save();
		OnPropertyChanged(nameof(TileMap));
	}

	public void ClearTile(Vector2I tileIndex) {
		var info = _tileMapInfos.Find(info => info.Contains(tileIndex));
		if (info == null) return;
		_tileMapInfos.Remove(info);

		var infoPosition = info.Position.ToTileIndex();
		var tile = Data.TileMap.FirstOrDefault(t => t.X == infoPosition.X && t.Y == infoPosition.Y);
		if (tile != null) {
			Data.TileMap.Remove(tile);
		}

		Save();
		OnPropertyChanged(nameof(TileMap));
	}
}