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

		TileSetIds = new ObservableList<Guid>(file, area.TileSetIds);
		Monsters = new MonsterDataInfoCollection(file, this, area.Monsters);
		TileMapLayers = new InfoList<TileMapLayerInfo, TileMapLayerData>(file, area.TileMapLayers, data => new TileMapLayerInfo(file, data, this));
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

	public ObservableList<Guid> TileSetIds { get; }
	public MonsterDataInfoCollection Monsters { get; }
	public ObservableCollectionInfo<TileMapLayerInfo, TileMapLayerData> TileMapLayers { get; }

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

	public TileInfo GetTileInfo(Guid tileId) => Level.GetTileInfo(tileId) ??
												TileSetIds.SelectMany(id => File.GetTileSet(id).TileSet)
														  .First(t => t.Id == tileId);

	public void SetTile(Vector2I tileIndex, int zIndex, Guid tileId) {
		var layer = GetOrCreateLayer(zIndex);
		var info = layer.TileMap.FirstOrDefault(info => info.Contains(tileIndex)) ?? layer.TileMap.Add(new TileMapData {
			X = tileIndex.X,
			Y = tileIndex.Y,
			TileId = tileId
		});

		info.TileId = tileId;
	}

	private TileMapLayerInfo GetOrCreateLayer(int zIndex) {
		return TileMapLayers.FirstOrDefault(l => l.ZIndex == zIndex) ?? TileMapLayers.Add(new TileMapLayerData {
			Name = zIndex == 0 ? "Default" : "Layer",
			ZIndex = zIndex
		});
	}

	public void ClearTile(Vector2I tileIndex, int zIndex) {
		var layer = GetOrCreateLayer(zIndex);
		var info = layer.TileMap.FirstOrDefault(info => info.Contains(tileIndex));
		if (info == null) return;
		layer.TileMap.Remove(info);
	}
}