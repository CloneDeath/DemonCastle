using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class TileToolsPanel : VBoxContainer {
	protected LevelInfo Level { get; }

	protected Button AddTileButton { get; }
	protected Button DeleteTileButton { get; }
	protected TileSelectorPanel TileSelector { get; }
	protected TileDetails TileDetails { get; }

	public TileToolsPanel(LevelInfo level) {
		Name = nameof(TileToolsPanel);
		Level = level;

		AddChild(AddTileButton = new Button { Text = "Add Tile" });
		AddTileButton.Pressed += AddTileButtonOnPressed;

		AddChild(DeleteTileButton = new Button { Text = "Delete Tile" });
		DeleteTileButton.Pressed += DeleteTileButtonOnPressed;

		var tileScroll = new ScrollContainer {
			VerticalScrollMode = ScrollContainer.ScrollMode.Auto,
			HorizontalScrollMode = ScrollContainer.ScrollMode.ShowNever,
			SizeFlagsVertical = SizeFlags.ExpandFill,
			CustomMinimumSize = new Vector2(0, 100)
		};
		AddChild(tileScroll);
		tileScroll.AddChild(TileSelector = new TileSelectorPanel(level.TileSet) {
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		TileSelector.TileSelected += TileSelector_OnTileSelected;

		AddChild(TileDetails = new TileDetails(level.Directory));
	}

	private void TileSelector_OnTileSelected(TileInfo? obj) {
		TileDetails.Proxy = obj;
	}

	public TileInfo? SelectedTile => TileSelector.SelectedTile;

	public override void _Process(double delta) {
		base._Process(delta);

		var itemSelected = TileSelector.SelectedTile != null;
		DeleteTileButton.Disabled = !itemSelected;
	}

	private void AddTileButtonOnPressed() {
		var tile = Level.TileSet.CreateTile();
		TileSelector.Reload();
		TileSelector.SelectedTile = tile;
		TileDetails.Proxy = tile;
	}

	private void DeleteTileButtonOnPressed() {
		var tile = TileSelector.SelectedTile;
		if (tile == null) return;
		Level.TileSet.DeleteTile(tile);
		TileSelector.Reload();
		TileDetails.Proxy = null;
	}
}