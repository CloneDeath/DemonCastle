using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class TileLayerEditor : HBoxContainer {
	private IEnumerableInfo<TileMapLayerInfo>? _layers;
	private readonly OptionButton _options;
	private readonly List<TileMapLayerInfo> _observed = new();

	public TileMapLayerInfo? SelectedLayer {
		get {
			var id = _options.GetSelectedId();
			return id == -1 ? null : _observed[id];
		}
	}

	public TileLayerEditor() {
		Name = nameof(TileLayerEditor);

		AddChild(new Label { Text = "Layers" });
		AddChild(_options = new OptionButton {
			Text = "<None>",
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		AddChild(new Button { Icon = IconTextures.AddIcon });
		AddChild(new Button { Icon = IconTextures.DeleteIcon });

		ReloadOptions();
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (_layers != null) _layers.CollectionChanged -= Layers_OnCollectionChanged;
	}

	private void Layers_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadOptions();
	}

	private void ReloadOptions() {
		var selectedLayerZIndex = SelectedLayer?.ZIndex;
		foreach (var layer in _observed) {
			layer.PropertyChanged -= Layer_OnPropertyChanged;
		}
		_observed.Clear();
		_options.Clear();

		_options.Disabled = _layers == null;
		if (_layers == null) {
			_options.Selected = -1;
			return;
		}

		_observed.AddRange(_layers.OrderBy(l => l.ZIndex));
		for (var i = 0; i < _observed.Count; i++) {
			var layer = _observed[i];
			layer.PropertyChanged += Layer_OnPropertyChanged;
			_options.AddItem($"{layer.ZIndex} - {layer.Name}", i);
		}

		if (!_observed.Any()) {
			_options.Selected = -1;
			_options.Text = "<None>";
		} else {
			var layer = _observed.FirstOrDefault(l => l.ZIndex == selectedLayerZIndex)
						?? ((IEnumerable<TileMapLayerInfo>)_observed).Reverse().MinBy(l => Math.Abs(l.ZIndex));
			_options.Selected = layer == null ? -1 : _observed.IndexOf(layer);
		}
	}

	private void Layer_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		ReloadOptions();
	}

	public void LoadLayers(IEnumerableInfo<TileMapLayerInfo>? layers) {
		if (_layers != null) _layers.CollectionChanged -= Layers_OnCollectionChanged;
		_layers = layers;
		if (_layers != null) _layers.CollectionChanged += Layers_OnCollectionChanged;
		ReloadOptions();
	}
}