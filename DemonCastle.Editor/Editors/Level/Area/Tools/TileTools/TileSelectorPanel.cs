using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Data.TileSets;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class TileSelectorPanel : HFlowContainer {
	private readonly List<SelectableTile> _selection = new();
	private TileInfo? _selectedTile;

	public TileInfoCollection TileSet { get; }

	public TileInfo? SelectedTile {
		get => _selectedTile;
		set {
			if (_selectedTile == value) return;
			_selectedTile = value;
			SelectTileInfo(value);
		}
	}

	public event Action<TileInfo?>? TileSelected;

	public TileSelectorPanel(TileInfoCollection tileSet) {
		TileSet = tileSet;
		Reload();
	}

	public override void _EnterTree() {
		base._EnterTree();
		TileSet.CollectionChanged += TileSet_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		TileSet.CollectionChanged -= TileSet_OnCollectionChanged;
	}

	private void SelectTileInfo(TileInfo? tile) {
		foreach (var selectableTile in _selection) {
			selectableTile.IsSelected = selectableTile.Tile == tile;
		}
		TileSelected?.Invoke(SelectedTile);
	}

	private void TileSet_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		Reload();
	}

	private void Reload() {
		foreach (var node in GetChildren()) {
			node.QueueFree();
		}
		_selection.Clear();

		foreach (var tile in TileSet) {
			var c = new SelectableTile(tile);
			AddChild(c);
			_selection.Add(c);
			c.Selected += OnTileSelected;
		}
	}

	private void OnTileSelected(SelectableControl selection) {
		if (selection is not SelectableTile selectableTile) return;
		SelectedTile = selectableTile.Tile;
		TileSelected?.Invoke(SelectedTile);
	}
}