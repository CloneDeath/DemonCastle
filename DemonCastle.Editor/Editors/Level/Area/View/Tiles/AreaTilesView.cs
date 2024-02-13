using System.Collections.Generic;
using System.Collections.Specialized;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.View.Tiles;

public partial class AreaTilesView : Control {
	private readonly AreaInfo _area;
	private readonly List<TileLayerView> _layers = new();
	private int _selectedLayerIndex;
	private bool _showSingleLayer;

	public AreaTilesView(AreaInfo area) {
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
	}

	public override void _ExitTree() {
		base._ExitTree();
		_area.TileMapLayers.CollectionChanged -= TileMapLayers_OnCollectionChanged;
	}

	private void TileMapLayers_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadArea();
	}

	private void ReloadArea() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
		_layers.Clear();

		foreach (var layer in _area.TileMapLayers) {
			var tileLayerView = new TileLayerView(layer) {
				MouseFilter = MouseFilterEnum.Pass
			};
			AddChild(tileLayerView);
			_layers.Add(tileLayerView);
		}

		RefreshLayerVisibility();
	}

	private void RefreshLayerVisibility() {
		foreach (var layer in _layers) {
			layer.Visible = !_showSingleLayer || layer.LayerIndex == _selectedLayerIndex;
		}
	}
}