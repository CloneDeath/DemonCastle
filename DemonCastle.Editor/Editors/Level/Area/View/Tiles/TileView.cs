using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels;

namespace DemonCastle.Editor.Editors.Level.Area.View.Tiles;

public partial class TileView : SpriteDefinitionView {
	protected TileMapInfo Tile { get; }

	public TileView(TileMapInfo tile) : base(tile.Sprite) {
		Tile = tile;

		Name = nameof(TileView);
		Position = tile.Position.ToPixelPositionInArea();
		LoadScale();
	}

	public override void _EnterTree() {
		base._EnterTree();
		Tile.PropertyChanged += Tile_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Tile.PropertyChanged -= Tile_OnPropertyChanged;
	}

	private void Tile_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		switch (e.PropertyName) {
			case nameof(Tile.Sprite):
				Load(Tile.Sprite);
				LoadScale();
				break;
			case nameof(Tile.Size):
				LoadScale();
				break;
		}
	}

	private void LoadScale() {
		Scale = Tile.TileScale * Tile.Size / Tile.Region.Size;
	}
}