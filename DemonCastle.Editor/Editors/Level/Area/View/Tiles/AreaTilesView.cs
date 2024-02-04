using System.Collections.Specialized;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.View.Tiles;

public partial class AreaTilesView : Control {
	private readonly AreaInfo _area;

	public AreaTilesView(AreaInfo area) {
		_area = area;
		ReloadArea();
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

		foreach (var layer in _area.TileMapLayers) {
			AddChild(new TileLayerView(layer) {
				MouseFilter = MouseFilterEnum.Pass
			});
		}
	}
}