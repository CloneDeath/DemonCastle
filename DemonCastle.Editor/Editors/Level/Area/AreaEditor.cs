using Godot;

namespace DemonCastle.Editor.Editors.Level.Area; 

public partial class AreaEditor : Control {
	private void AreaTileEditorOnTileCellSelected(Vector2I cell) {
		var selectedTile = TileSelector.SelectedTile;
		if (selectedTile == null) return;

		AreaTileEditor.SetTile(cell, selectedTile.Name);
	}
	
	private void AreaTileEditorOnTileCellCleared(Vector2I cell) {
		AreaTileEditor.ClearTile(cell);
	}
}