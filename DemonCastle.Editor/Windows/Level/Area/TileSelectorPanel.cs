using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class TileSelectorPanel : HFlowContainer {
	private readonly List<SelectableTile> _selection = new();
	public TileInfo? SelectedTile;
	
	public TileSelectorPanel(LevelTileSet tileSet) {
		foreach (var tile in tileSet.Tiles) {
			var c = new SelectableTile(tile);
			AddChild(c);
			_selection.Add(c);
			c.Selected += OnTileSelected;
		}
	}

	private void OnTileSelected(SelectableTile selection) {
		SelectedTile = selection.Tile;
		foreach (var selectableTile in _selection.Where(selectableTile => selectableTile != selection)) {
			selectableTile.ClearSelection();
		}
	}
}