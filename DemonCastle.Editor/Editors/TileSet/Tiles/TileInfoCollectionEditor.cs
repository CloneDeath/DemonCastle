using System;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Editor.Editors.TileSet.Tiles;

public partial class TileInfoCollectionEditor : VBoxContainer {
	private readonly IEnumerableInfo<TileInfo> _tiles;

	public TileInfo? SelectedTile => TileSelector.SelectedTile;
	public event Action<TileInfo?>? TileSelected;

	protected Button AddTileButton { get; }
	protected Button DeleteTileButton { get; }
	protected TileSelectorPanel TileSelector { get; }

	public TileInfoCollectionEditor(IEnumerableInfo<TileInfo> tiles) {
		_tiles = tiles;
		Name = nameof(TileInfoCollectionEditor);

		AddChild(AddTileButton = new Button { Text = "Add Tile" });
		AddTileButton.Pressed += AddTileButton_OnPressed;

		AddChild(DeleteTileButton = new Button { Text = "Delete Tile" });
		DeleteTileButton.Pressed += DeleteTileButton_OnPressed;

		var tileScroll = new ScrollContainer {
			VerticalScrollMode = ScrollContainer.ScrollMode.Auto,
			HorizontalScrollMode = ScrollContainer.ScrollMode.ShowNever,
			SizeFlagsVertical = SizeFlags.ExpandFill,
			CustomMinimumSize = new Vector2(0, 100)
		};
		AddChild(tileScroll);
		tileScroll.AddChild(TileSelector = new TileSelectorPanel(tiles) {
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		TileSelector.TileSelected += TileSelector_OnTileSelected;
	}

	private void TileSelector_OnTileSelected(TileInfo? tile) {
		TileSelected?.Invoke(tile);
	}

	private void AddTileButton_OnPressed() {
		var tile = _tiles.AppendNew();
		TileSelector.SelectedTile = tile;
		TileSelected?.Invoke(tile);
	}

	private void DeleteTileButton_OnPressed() {
		var tile = TileSelector.SelectedTile;
		if (tile == null) return;
		_tiles.Remove(tile);
		TileSelector.SelectedTile = null;
		TileSelected?.Invoke(null);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		var itemSelected = TileSelector.SelectedTile != null;
		DeleteTileButton.Disabled = !itemSelected;
	}
}