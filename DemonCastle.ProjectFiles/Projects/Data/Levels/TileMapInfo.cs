using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Locations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels;

public class TileMapInfo : INotifyPropertyChanged {
	protected TileMapData TileMapData { get; }
	protected AreaInfo AreaInfo { get; }

	public TileInfo Tile => AreaInfo.GetTileInfo(TileId);
	public ISpriteDefinition Sprite => Tile.Sprite;

	public TileMapInfo(TileMapData tileMapData, AreaInfo areaInfo) {
		TileMapData = tileMapData;
		AreaInfo = areaInfo;
		Tile.PropertyChanged += Tile_OnPropertyChanged;
	}

	private void Tile_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		switch (e.PropertyName) {
			case nameof(Tile.Sprite):
				OnPropertyChanged(nameof(Sprite));
				break;
			case nameof(Tile.Span):
				OnPropertyChanged(nameof(Span));
				break;
		}
	}

	protected LevelTileSet TileSet => AreaInfo.LevelTileSet;
	protected Vector2I TileIndex => new(TileMapData.X, TileMapData.Y);

	public Vector2I TileScale => AreaInfo.TileSize;
	public TilePosition Position => new(TileIndex, AreaInfo.PositionOfArea, TileScale);
	public Guid TileId {
		get => TileMapData.TileId;
		set => TileMapData.TileId = value;
	}

	public Vector2I Span => Tile.Span;
	public Rect2 Region => Sprite.Region;

	public bool Contains(Vector2I tileIndex) {
		var bounds = new Rect2I(Position.ToTileIndex(), Span);
		return bounds.HasPoint(tileIndex);
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