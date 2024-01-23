using System;
using System.ComponentModel;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Locations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;

public class TileMapInfo : BaseInfo<TileMapData> {
	protected AreaInfo AreaInfo { get; }

	public TileInfo Tile => AreaInfo.GetTileInfo(TileId);
	public ISpriteDefinition Sprite => Tile.Sprite;

	public TileMapInfo(IFileNavigator file, TileMapData data, AreaInfo areaInfo) : base(file, data) {
		AreaInfo = areaInfo;
		Tile.PropertyChanged += Tile_OnPropertyChanged;
	}

	private void Tile_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		switch (e.PropertyName) {
			case nameof(Tile.Sprite):
				OnPropertyChanged(nameof(Sprite));
				break;
			case nameof(Tile.Size):
				OnPropertyChanged(nameof(Size));
				break;
		}
	}

	protected Vector2I TileIndex => new(Data.X, Data.Y);

	public Vector2I TileScale => AreaInfo.TileSize;
	public TilePosition Position => new(TileIndex, AreaInfo.PositionOfArea, TileScale);
	public Guid TileId {
		get => Data.TileId;
		set => Data.TileId = value;
	}

	public Vector2I Size => Tile.Size;
	public Rect2 Region => Sprite.Region;

	public bool Contains(Vector2I tileIndex) {
		var bounds = new Rect2I(Position.ToTileIndex(), Size);
		return bounds.HasPoint(tileIndex);
	}
}