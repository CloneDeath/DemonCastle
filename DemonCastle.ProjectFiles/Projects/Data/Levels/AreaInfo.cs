using System;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Locations;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels;

public class AreaInfo : BaseInfo<AreaData> {
	public LevelInfo Level { get; }

	public AreaInfo(IFileNavigator file, AreaData area, LevelInfo level) : base(file, area) {
		Level = level;
		Monsters = new MonsterDataInfoCollection(file, this, area.Monsters);
		TileMapLayers = new InfoList<TileMapLayerInfo, TileMapLayerData>(file, area.TileMapLayers, data => new TileMapLayerInfo(file, data));
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
	public MonsterDataInfoCollection Monsters { get; }
	public IEnumerableInfo<TileMapLayerInfo> TileMapLayers { get; }

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

	public void SetTile(Vector2I tileIndex, int zIndex, Guid tileId) {
		var layer = GetOrCreateLayer(zIndex);
		var info = layer.TileMap.FirstOrDefault(info => info.Contains(tileIndex));
		if (info != null) {
			info.TileId = tileId;
		}
		else {
			var tile = layer.TileMap.AppendNew();
			tile.Position = tileIndex;
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

	private TileMapLayerInfo GetOrCreateLayer(int zIndex) {
		var layer = TileMapLayers.FirstOrDefault(l => l.ZIndex == zIndex) ?? TileMapLayers.AppendNew();
		layer.ZIndex = zIndex;
		return layer;
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