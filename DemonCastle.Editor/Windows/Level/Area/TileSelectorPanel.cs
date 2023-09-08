using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class TileSelectorPanel : HFlowContainer {
	public LevelTileSet TileSet { get; }
	private readonly List<SelectableTile> _selection = new();
	public TileInfo? SelectedTile;
	
	public TileSelectorPanel(LevelTileSet tileSet) {
		TileSet = tileSet;
		Reload();
	}

	private void OnTileSelected(SelectableTile selection) {
		SelectedTile = selection.Tile;
		foreach (var selectableTile in _selection.Where(selectableTile => selectableTile != selection)) {
			selectableTile.ClearSelection();
		}
	}

	public void Reload() {
		foreach (var node in GetChildren()) {
			node.QueueFree();
		}
		
		foreach (var tile in TileSet.Tiles) {
			var c = new SelectableTile(tile);
			AddChild(c);
			_selection.Add(c);
			c.Selected += OnTileSelected;
		}
	}
}