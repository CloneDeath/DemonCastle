using System;
using System.Collections.Generic;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class TileSelectorPanel : HFlowContainer {
	private readonly List<SelectableTile> _selection = new();
	private TileInfo? _selectedTile;

	public LevelTileSet TileSet { get; }

	public TileInfo? SelectedTile {
		get => _selectedTile;
		set {
			if (_selectedTile == value) return;
			_selectedTile = value;
			SelectTileInfo(value);
		}
	}

	public event Action<TileInfo?>? TileSelected;

	public TileSelectorPanel(LevelTileSet tileSet) {
		TileSet = tileSet;
		Reload();
	}

	private void OnTileSelected(SelectableControl selection) {
		if (selection is not SelectableTile selectableTile) return;
		SelectedTile = selectableTile.Tile;
		TileSelected?.Invoke(SelectedTile);
	}

	private void SelectTileInfo(TileInfo? tile) {
		foreach (var selectableTile in _selection) {
			selectableTile.IsSelected = selectableTile.Tile == tile;
		}
		TileSelected?.Invoke(SelectedTile);
	}

	public void Reload() {
		foreach (var node in GetChildren()) {
			node.QueueFree();
		}
		_selection.Clear();

		foreach (var tile in TileSet.Tiles) {
			var c = new SelectableTile(tile);
			AddChild(c);
			_selection.Add(c);
			c.Selected += OnTileSelected;
		}
	}
}