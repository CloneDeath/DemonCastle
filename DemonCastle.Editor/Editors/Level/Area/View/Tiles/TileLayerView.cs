using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.View.Tiles;

public partial class TileLayerView : Control {
	private readonly TileMapLayerInfo _layer;
	private readonly Godot.Collections.Dictionary<Vector2I, TileView> _tileMap = new();
	public event Action<TileLayerView>? LayerIndexChanged;

	public int LayerIndex => _layer.ZIndex;

	public TileLayerView(TileMapLayerInfo layer) {
		Name = $"{nameof(TileLayerView)}@{layer.ZIndex} ({layer.Name})";
		_layer = layer;
		ReloadLayer();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_layer.PropertyChanged += Layer_OnPropertyChanged;
		_layer.TileMap.CollectionChanged += TileMap_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_layer.PropertyChanged -= Layer_OnPropertyChanged;
		_layer.TileMap.CollectionChanged -= TileMap_OnCollectionChanged;
	}

	private void Layer_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName == nameof(_layer.ZIndex)) {
			LayerIndexChanged?.Invoke(this);
		}
	}

	private void TileMap_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		AddTiles(e.NewItems?.Cast<TileMapInfo>() ?? Array.Empty<TileMapInfo>());
		RemoveTiles(e.OldItems?.Cast<TileMapInfo>() ?? Array.Empty<TileMapInfo>());
	}

	private void ReloadLayer() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
		_tileMap.Clear();
		AddTiles(_layer.TileMap);
	}

	private void AddTiles(IEnumerable<TileMapInfo> tiles) {
		foreach (var tile in tiles) {
			AddTile(tile);
		}
	}

	private void RemoveTiles(IEnumerable<TileMapInfo> tiles) {
		foreach (var tile in tiles) {
			var index = tile.Position.ToTileIndex();
			var tileView = _tileMap[index];
			tileView.QueueFree();
			_tileMap.Remove(index);
		}
	}

	private void AddTile(TileMapInfo tile) {
		var tileView = new TileView(tile);
		_tileMap[tile.Position.ToTileIndex()] = tileView;
		AddChild(tileView);
	}
}