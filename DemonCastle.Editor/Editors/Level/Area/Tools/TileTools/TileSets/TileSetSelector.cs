using System;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.TileSets;

public partial class TileSetSelector : VBoxContainer {
	private readonly ProjectResources _resources;
	protected AddRemoveOptionButton Options { get; }
	protected FileDialog OpenFileDialog { get; }

	private ObservableList<Guid>? _tileSets;

	public event Action<Guid?>? TileSetIdSelected;

	public Guid? SelectedGuid {
		get {
			if (Options.Selected < 0) return null;
			var metadata = Options.GetItemMetadata(Options.Selected).ToString();
			return Guid.Parse(metadata);
		}
		set {
			if (value == null) {
				Options.Selected = -1;
				CheckRemoveButton();
				return;
			}
			for (var i = 0; i < Options.ItemCount; i++) {
				var metadata = Options.GetItemMetadata(i).ToString();
				var guid = Guid.Parse(metadata);
				if (guid != value) continue;
				Options.Selected = i;
				CheckRemoveButton();
				return;
			}
		}
	}

	public TileSetSelector(ProjectResources resources, string directory) {
		_resources = resources;
		Name = nameof(TileSetSelector);

		AddChild(Options = new AddRemoveOptionButton {
			Label = "Tile Set",
			Disabled = true
		});
		Options.AddPressed += Options_OnAddPressed;
		Options.RemovePressed += Options_OnRemovePressed;
		Options.ItemSelected += Options_OnItemSelected;

		AddChild(OpenFileDialog = new FileDialog {
			Filters = new[] { FileType.TileSet.Filter },
			FileMode = FileDialog.FileModeEnum.OpenFile,
			Exclusive = true,
			Access = FileDialog.AccessEnum.Filesystem,
			Size = new Vector2I(800, 600),
			Unresizable = false,
			Title = "Select Tile Set",
			CurrentDir = directory
		});
		OpenFileDialog.FileSelected += OpenFileDialog_OnFileSelected;
	}

	private void Options_OnAddPressed() => OpenFileDialog.Popup();

	private void Options_OnRemovePressed() {
		if (SelectedGuid == null || SelectedGuid == Guid.Empty) return;
		_tileSets?.Remove(SelectedGuid.Value);
	}

	private void OpenFileDialog_OnFileSelected(string filePath) {
		if (_tileSets == null) return;
		var tileSet = _resources.GetTileSet(filePath);
		if (!_tileSets.Contains(tileSet.Id)) {
			_tileSets.Add(tileSet.Id);
		}
		SelectedGuid = tileSet.Id;
		TileSetIdSelected?.Invoke(tileSet.Id);
	}

	private void Options_OnItemSelected(long index) {
		CheckRemoveButton();
		TileSetIdSelected?.Invoke(SelectedGuid);
	}

	private void CheckRemoveButton() {
		Options.RemoveDisabled = SelectedGuid == null || SelectedGuid == Guid.Empty;
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
		Options.Disabled = _tileSets == null;
	}

	private void Reload() {
		Options.Clear();

		Options.AddItem("[Level Tiles]");
		Options.SetItemMetadata(0, Guid.Empty.ToString());
		CheckRemoveButton();
		TileSetIdSelected?.Invoke(SelectedGuid);

		if (_tileSets == null) return;

		foreach (var tileSetIds in _tileSets) {
			var tileSet = _resources.GetTileSet(tileSetIds);
			Options.AddItem(tileSet?.Name ?? "[TileSet Not Found!]");
			Options.SetItemMetadata(Options.ItemCount - 1, tileSetIds.ToString());
		}
	}
}