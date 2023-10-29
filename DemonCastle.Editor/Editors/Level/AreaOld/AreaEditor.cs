using Godot;

namespace DemonCastle.Editor.Editors.Level.AreaOld;

public partial class AreaEditor : BaseEditor {
	private void AreaTileEditorOnTileCellSelected(Vector2I cell) {
		var selectedTile = TileSelector.SelectedTile;
		if (selectedTile == null) return;

		AreaTileEditor.SetTile(cell, selectedTile.Id);
	}

	private void AreaTileEditorOnTileCellCleared(Vector2I cell) {
		AreaTileEditor.ClearTile(cell);
	}
}