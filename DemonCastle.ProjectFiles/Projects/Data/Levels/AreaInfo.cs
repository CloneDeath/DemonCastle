using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Locations;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels;

public class AreaInfo : INotifyPropertyChanged {
	private readonly List<TileMapInfo> _tileMapInfos = new();
	public AreaInfo(AreaData area, LevelInfo levelInfo) {
		Area = area;
		LevelInfo = levelInfo;
		_tileMapInfos.AddRange(Area.TileMap.Select(tm => new TileMapInfo(tm, this)));
	}

	protected AreaData Area { get; }
	public LevelInfo LevelInfo { get; }

	public Guid Id => Area.Id;

	public string Name {
		get => Area.Name;
		set {
			Area.Name = value;
			LevelInfo.Save();
			OnPropertyChanged();
		}
	}

	public LevelTileSet LevelTileSet => LevelInfo.TileSet;
	public IEnumerable<TileMapInfo> TileMap => _tileMapInfos;

	public Vector2I AreaPosition {
		get => new(Area.X, Area.Y);
		set {
			Area.X = value.X;
			Area.Y = value.Y;
			LevelInfo.Save();
			OnPropertyChanged();
		}
	}

	public Vector2I Size {
		get => new(Area.Width, Area.Height);
		set {
			Area.Width = value.X;
			Area.Height = value.Y;
			LevelInfo.Save();
			OnPropertyChanged();
		}
	}

	public AreaPosition PositionOfArea => new(AreaPosition, LevelInfo.AreaSize, LevelInfo.TileSize);
	public AreaSize SizeOfArea => new(Size, LevelInfo.AreaSize, LevelInfo.TileSize);
	public AreaRegion Region => new(PositionOfArea, SizeOfArea);

	public Vector2I TilePosition => AreaPosition * LevelInfo.TileSize * LevelInfo.AreaSize;

	public Vector2I TileSize => LevelInfo.TileSize;
	public Vector2I AreaSize => LevelInfo.AreaSize;
	public LevelTileSet TileSet => LevelInfo.TileSet;

	public TileInfo GetTileInfo(Guid tileId) => LevelInfo.GetTileInfo(tileId);

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
			Area.TileMap.Add(tileMapData);
			_tileMapInfos.Add(new TileMapInfo(tileMapData, this));
		}

		OnPropertyChanged(nameof(TileMap));
		LevelInfo.Save();
	}

	public void ClearTile(Vector2I tileIndex) {
		var info = _tileMapInfos.Find(info => info.Contains(tileIndex));
		if (info == null) return;
		_tileMapInfos.Remove(info);

		var infoPosition = info.Position.ToTileIndex();
		var tile = Area.TileMap.FirstOrDefault(t => t.X == infoPosition.X && t.Y == infoPosition.Y);
		if (tile != null) {
			Area.TileMap.Remove(tile);
		}

		OnPropertyChanged(nameof(TileMap));
		LevelInfo.Save();
	}

	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	#endregion
}