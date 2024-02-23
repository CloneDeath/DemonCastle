using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.View.Tiles;

public partial class AreaTilesView : Control {
	private readonly AreaInfo _area;
	private readonly List<TileLayerView> _layers = new();
	private int _selectedLayerIndex;
	private bool _showSingleLayer;

	public AreaTilesView(AreaInfo area) {
		Name = nameof(AreaTilesView);

		_area = area;
		ReloadArea();
	}

	public bool ShowSingleLayer {
		get => _showSingleLayer;
		set {
			_showSingleLayer = value;
			RefreshLayerVisibility();
		}
	}

	public int SelectedLayerIndex {
		get => _selectedLayerIndex;
		set {
			_selectedLayerIndex = value;
			RefreshLayerVisibility();
		}
	}

	public override void _EnterTree() {
		base._EnterTree();
		_area.TileMapLayers.CollectionChanged += TileMapLayers_OnCollectionChanged;
		_area.TileSetIds.CollectionChanged += TileSetIds_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_area.TileMapLayers.CollectionChanged -= TileMapLayers_OnCollectionChanged;
		_area.TileSetIds.CollectionChanged -= TileSetIds_OnCollectionChanged;

	}

	private void TileMapLayers_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadArea();
	}

	private void TileSetIds_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadArea();
	}

	private void ReloadArea() {
		foreach (var child in _layers) {
			child.LayerIndexChanged -= Child_OnLayerIndexChanged;
			child.QueueFree();
		}
		_layers.Clear();

		foreach (var layer in _area.TileMapLayers.OrderBy(l => l.ZIndex)) {
			var tileLayerView = new TileLayerView(layer) {
				MouseFilter = MouseFilterEnum.Pass
			};
			tileLayerView.LayerIndexChanged += Child_OnLayerIndexChanged;
			AddChild(tileLayerView);
			_layers.Add(tileLayerView);
		}

		RefreshLayerVisibility();
	}

	private void Child_OnLayerIndexChanged(TileLayerView layer) {
		var newIndex = _layers.OrderBy(l => l.ZIndex).Where(l => l != layer).ToList().FindIndex(l => l.LayerIndex > layer.LayerIndex);
		MoveChild(layer, newIndex);
	}

	private void RefreshLayerVisibility() {
		foreach (var layer in _layers) {
			layer.Visible = !_showSingleLayer || layer.LayerIndex == _selectedLayerIndex;
		}
	}
}