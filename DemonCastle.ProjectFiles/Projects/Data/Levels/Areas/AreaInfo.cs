using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Locations;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;

public class AreaInfo : BaseInfo<AreaData> {
	public LevelInfo Level { get; }

	public AreaInfo(IFileNavigator file, AreaData area, LevelInfo level) : base(file, area) {
		Level = level;

		TileSetIds = new ObservableList<Guid>(file, area.TileSetIds);
		Monsters = new MonsterDataInfoCollection(file, this, area.Monsters);
		TileMapLayers = new InfoList<TileMapLayerInfo, TileMapLayerData>(file, area.TileMapLayers, data => new TileMapLayerInfo(file, data, this));

		TileSetIds.CollectionChanged += TileSetIds_OnCollectionChanged;
	}

	private void TileSetIds_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		_tileCache.Clear();
	}

	public Guid Id => Data.Id;

	public string Name {
		get => Data.Name;
		set => SaveField(ref Data.Name, value);
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

	private readonly Dictionary<Guid, ITileInfo> _tileCache = new();
	public ITileInfo GetTileInfo(Guid tileId) {
		var cache = _tileCache.GetValueOrDefault(tileId);
		if (cache != null) return cache;

		var value = (ITileInfo?)Level.GetTileInfo(tileId)
					?? TileSetIds.SelectMany(id =>
									 File.GetTileSet(id)?.TileSet ?? new NullEnumerableInfo<TileInfo>())
								 .FirstOrDefault(t => t.Id == tileId);
		if (value == null) return new NullTileInfo(tileId);
		_tileCache[tileId] = value;
		return value;
	}

	public void SetTile(Vector2I tileIndex, int zIndex, Guid tileId) {
		if (!Region.ContainsTileIndex(tileIndex)) return;
		var layer = GetOrCreateLayer(zIndex);
		var info = layer.TileMap.FirstOrDefault(info => info.Contains(tileIndex)) ?? layer.TileMap.Add(new TileMapData {
			X = tileIndex.X,
			Y = tileIndex.Y,
			TileId = tileId
		});

		info.TileId = tileId;
	}

	public void FloodFillTile(Vector2I cell, int selectedLayerZIndex, Guid selectedTileId) {
		var layer = TileMapLayers.FirstOrDefault(l => l.ZIndex == selectedLayerZIndex);
		if (layer == null) return;

		var tileId = layer.TileMap.FirstOrDefault(info => info.Contains(cell))?.TileId;
		if (tileId == selectedTileId) return;

		var stack = new Stack<Vector2I>();
		stack.Push(cell);

		while (stack.Any()) {
			var current = stack.Pop();
			if (!Region.ContainsTileIndex(current)) continue;

			var currentTile = layer.TileMap.FirstOrDefault(info => info.Contains(current));
			if (currentTile?.TileId != tileId) continue;

			SetTile(current, selectedLayerZIndex, selectedTileId);

			stack.Push(current + Vector2I.Up);
			stack.Push(current + Vector2I.Down);
			stack.Push(current + Vector2I.Left);
			stack.Push(current + Vector2I.Right);
		}
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