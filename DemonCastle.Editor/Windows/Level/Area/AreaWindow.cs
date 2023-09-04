using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class AreaWindow : BaseWindow {
	private void AreaTileEditorOnTileCellSelected(Vector2I cell) {
		var selectedTile = TileSelector.SelectedTile;
		if (selectedTile == null) return;

		AreaTileEditor.SetTile(cell, selectedTile.Name);
	}
}