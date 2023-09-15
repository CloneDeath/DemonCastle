using DemonCastle.Editor.Extensions;
using DemonCastle.Editor.Windows.Level.TileMap;
using Godot;

namespace DemonCastle.Editor.Windows.Level; 

public partial class LevelEditor : Control {
	public override void _Process(double delta) {
		base._Process(delta);

		var itemSelected = TileSelector.SelectedTile != null;
		EditTileButton.Disabled = !itemSelected;
		DeleteTileButton.Disabled = !itemSelected;
	}

	private void AddAreaButtonOnPressed() {
		var area = LevelInfo.CreateArea();
		var editor = new Area.AreaEditor(area);
		this.GetEditArea().ShowEditor(editor);
		AreaEditor.Reload();
	}

	private void AddTileButtonOnPressed() {
		var tile = LevelInfo.TileSet.CreateTile();
		var editor = new TileEditor(tile);
		this.GetEditArea().ShowEditor(editor);
		TileSelector.Reload();
	}

	private void EditTileButtonOnPressed() {
		var tile = TileSelector.SelectedTile;
		if (tile == null) return;
		var window = new TileEditor(tile);
		this.GetEditArea().ShowEditor(window);
	}

	private void DeleteTileButtonOnPressed() {
		var tile = TileSelector.SelectedTile;
		if (tile == null) return;
		LevelInfo.TileSet.DeleteTile(tile);
		TileSelector.Reload();
	}
}