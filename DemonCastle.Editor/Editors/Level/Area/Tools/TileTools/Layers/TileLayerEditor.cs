using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.Layers;

public partial class TileLayerEditor : VBoxContainer {
	private IEnumerableInfo<TileMapLayerInfo>? _layers;
	private readonly List<TileMapLayerInfo> _observed = new();

	private readonly OptionButton _options;
	private readonly Button _addButton;
	private readonly Button _deleteButton;
	private readonly TileLayerDetails _details;

	public TileMapLayerInfo? SelectedLayer {
		get {
			var id = _options.GetSelectedId();
			return id == -1 ? null : _observed[id];
		}
	}

	public TileLayerEditor() {
		Name = nameof(TileLayerEditor);

		HBoxContainer top;
		AddChild(top = new HBoxContainer());

		top.AddChild(new Label { Text = "Layers" });
		top.AddChild(_options = new OptionButton {
			Text = "<None>",
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		_options.ItemSelected += Options_OnItemSelected;
		top.AddChild(_addButton = new Button { Icon = IconTextures.AddIcon });
		_addButton.Pressed += AddButton_OnPressed;
		top.AddChild(_deleteButton = new Button { Icon = IconTextures.DeleteIcon });
		_deleteButton.Pressed += DeleteButton_OnPressed;

		AddChild(_details = new TileLayerDetails());

		ReloadOptions();
	}

	private void Options_OnItemSelected(long index) {
		_deleteButton.Disabled = _layers == null || index < 0;
		_details.Layer = SelectedLayer;
	}

	private void AddButton_OnPressed() {
		var layer = _layers?.AppendNew();
		_details.Layer = layer;
		_options.Selected = layer == null ? -1 : _observed.IndexOf(layer);
	}

	private void DeleteButton_OnPressed() {
		if (SelectedLayer == null) return;
		_layers?.Remove(SelectedLayer);
		_details.Layer = null;
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (_layers != null) _layers.CollectionChanged -= Layers_OnCollectionChanged;
		foreach (var layer in _observed) {
			layer.PropertyChanged -= Layer_OnPropertyChanged;
		}
		_observed.Clear();
	}

	private void Layers_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadOptions();
	}

	private void ReloadOptions() {
		var selectedLayer = SelectedLayer;
		foreach (var layer in _observed) {
			layer.PropertyChanged -= Layer_OnPropertyChanged;
		}
		_observed.Clear();
		_options.Clear();

		_addButton.Disabled = _layers == null;
		_deleteButton.Disabled = _layers == null;
		_options.Disabled = _layers == null;
		if (_layers == null) {
			_details.Layer = null;
			_options.Selected = -1;
			return;
		}

		_observed.AddRange(_layers.OrderBy(l => l.ZIndex));
		for (var i = 0; i < _observed.Count; i++) {
			var layer = _observed[i];
			layer.PropertyChanged += Layer_OnPropertyChanged;
			_options.AddItem($"[{layer.ZIndex}] {layer.Name}", i);
		}

		if (!_observed.Any()) {
			_options.Selected = -1;
			_options.Text = "<None>";
			_details.Layer = null;
		} else {
			var layer = selectedLayer
						?? _observed.FirstOrDefault(l => l.ZIndex == selectedLayer?.ZIndex)
						?? ((IEnumerable<TileMapLayerInfo>)_observed).Reverse().MinBy(l => Math.Abs(l.ZIndex));
			_options.Selected = layer == null ? -1 : _observed.IndexOf(layer);
			_details.Layer = layer;
		}
		_deleteButton.Disabled = _options.Selected < 0;
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