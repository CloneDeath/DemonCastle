using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class TileSelectorPanel : HFlowContainer {
	private readonly List<SelectableTile> _selection = new();
	
	public TileSelectorPanel(LevelTileSet tileSet) {
		foreach (var tile in tileSet.Tiles) {
			var c = new SelectableTile(tile);
			AddChild(c);
			_selection.Add(c);
			c.Selected += OnTileSelected;
		}
	}

	private void OnTileSelected(SelectableTile selection) {
		foreach (var selectableTile in _selection) {
			if (selectableTile == selection) continue;
			selectableTile.ClearSelection();
		}
	}
}