using System;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.TileSets;

public partial class TileSetSelector : VBoxContainer {
	private readonly AddRemoveOptionButton _options;

	private readonly ProjectInfo _project;
	private ObservableList<Guid>? _tileSets;

	public Guid? SelectedGuid {
		get {
			if (_options.Selected < 0) return null;
			var metadata = _options.GetItemMetadata(_options.Selected).ToString();
			return Guid.Parse(metadata);
		}
	}

	public TileSetSelector(ProjectInfo project) {
		_project = project;
		Name = nameof(TileSetSelector);

		AddChild(_options = new AddRemoveOptionButton {
			Label = "Tile Set"
		});
		_options.ItemSelected += Options_OnItemSelected;
	}

	private void Options_OnItemSelected(long index) {
		CheckRemoveButton();
	}

	private void CheckRemoveButton() {
		_options.RemoveDisabled = SelectedGuid == null || SelectedGuid == Guid.Empty;
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (_tileSets != null) _tileSets.CollectionChanged -= TileSets_OnCollectionChanged;
	}

	private void TileSets_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		Reload();
	}

	public void LoadArea(AreaInfo? area) {
		if (_tileSets != null) _tileSets.CollectionChanged -= TileSets_OnCollectionChanged;
		_tileSets = area?.TileSetIds;
		if (_tileSets != null) _tileSets.CollectionChanged += TileSets_OnCollectionChanged;
		Reload();
	}

	private void Reload() {
		_options.Clear();

		_options.AddItem("[Level Tiles]");
		_options.SetItemMetadata(0, Guid.Empty.ToString());
		CheckRemoveButton();

		if (_tileSets == null) return;

		var tileSets = _project.TileSets.ToList();
		foreach (var guid in _tileSets) {
			var tileSet = tileSets.FirstOrDefault(t => t.Id == guid);
			_options.AddItem(tileSet?.Name ?? "[TileSet Not Found!]");
			_options.SetItemMetadata(_options.ItemCount - 1, guid.ToString());
		}
	}
}