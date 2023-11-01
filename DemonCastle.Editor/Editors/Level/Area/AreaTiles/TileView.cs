using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class TileView : SpriteDefinitionView {
	protected TileMapInfo Tile { get; }

	public TileView(TileMapInfo tile) : base(tile.Sprite) {
		Tile = tile;

		Name = nameof(TileView);
		Position = tile.Position.ToPixelPositionInArea();
		LoadScale();

		tile.PropertyChanged += Tile_OnPropertyChanged;
	}

	private void Tile_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		switch (e.PropertyName) {
			case nameof(Tile.Sprite):
				Load(Tile.Sprite);
				break;
			case nameof(Tile.Span):
				LoadScale();
				break;
		}
	}

	private void LoadScale() {
		Scale = Tile.TileScale * Tile.Span / Tile.Region.Size;
	}
}