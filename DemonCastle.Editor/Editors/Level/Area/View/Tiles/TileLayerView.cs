using System.Collections.Specialized;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.View.Tiles;

public partial class TileLayerView : Control {
	private readonly TileMapLayerInfo _layer;

	public TileLayerView(TileMapLayerInfo layer) {
		_layer = layer;
		ReloadLayer();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_layer.TileMap.CollectionChanged += TileMap_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_layer.TileMap.CollectionChanged -= TileMap_OnCollectionChanged;
	}

	private void TileMap_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadLayer();
	}

	private void ReloadLayer() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		foreach (var tile in _layer.TileMap) {
			var tileView = new TileView(tile);
			AddChild(tileView);
		}
	}
}