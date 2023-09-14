using DemonCastle.Editor.Extensions;
using DemonCastle.Editor.Windows.Level.Area;
using DemonCastle.Editor.Windows.Level.TileMap;

namespace DemonCastle.Editor.Windows.Level; 

public partial class LevelWindow : BaseWindow {
	public override void _Process(double delta) {
		base._Process(delta);

		var itemSelected = TileSelector.SelectedTile != null;
		EditTileButton.Disabled = !itemSelected;
		DeleteTileButton.Disabled = !itemSelected;
	}

	private void AddAreaButtonOnPressed() {
		var area = LevelInfo.CreateArea();
		var window = new AreaWindow(area);
		this.GetWindowContainer().ShowWindow(window);
		AreaEditor.Reload();
	}

	private void AddTileButtonOnPressed() {
		var tile = LevelInfo.TileSet.CreateTile();
		var window = new TileWindow(tile);
		this.GetWindowContainer().ShowWindow(window);
		TileSelector.Reload();
	}

	private void EditTileButtonOnPressed() {
		var tile = TileSelector.SelectedTile;
		if (tile == null) return;
		var window = new TileWindow(tile);
		this.GetWindowContainer().ShowWindow(window);
	}

	private void DeleteTileButtonOnPressed() {
		var tile = TileSelector.SelectedTile;
		if (tile == null) return;
		LevelInfo.TileSet.DeleteTile(tile);
		TileSelector.Reload();
	}
}