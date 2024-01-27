using System;
using System.Linq;
using DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.TileSets;
using DemonCastle.Editor.Editors.TileSet.Tiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class TileToolsPanel : VBoxContainer {
	private readonly ProjectInfo _project;
	protected LevelInfo Level { get; }

	protected readonly Layers.TileLayerEditor _tileLayerEditor;
	protected TileSetSelector TileSetSelector { get; }

	protected VBoxContainer LevelTiles { get; }
	protected TileInfoCollectionEditor TileCollectionEditor { get; }
	protected LevelTileDetails TileDetails { get; }

	protected VBoxContainer AreaTiles { get; }

	public TileToolsPanel(ProjectInfo project, LevelInfo level) {
		_project = project;
		Name = nameof(TileToolsPanel);
		Level = level;

		AddChild(_tileLayerEditor = new Layers.TileLayerEditor());
		AddChild(TileSetSelector = new TileSetSelector(project, level.Directory));
		TileSetSelector.TileSetIdSelected += TileSetSelector_OnTileSetIdSelected;

		AddChild(LevelTiles = new VBoxContainer {
			SizeFlagsVertical = SizeFlags.ExpandFill,
			Visible = false
		});
		{
			LevelTiles.AddChild(TileCollectionEditor = new TileInfoCollectionEditor(level.TileSet));
			TileCollectionEditor.TileSelected += TileCollectionEditor_OnTileSelected;
			LevelTiles.AddChild(TileDetails = new LevelTileDetails(level.Directory));
		}

		AddChild(AreaTiles = new VBoxContainer {
			SizeFlagsVertical = SizeFlags.ExpandFill,
			Visible = false
		});
	}

	private void TileSetSelector_OnTileSetIdSelected(Guid? id) {
		LevelTiles.Visible = id == Guid.Empty;
		foreach (var child in AreaTiles.GetChildren()) {
			child.QueueFree();
		}
		if (id != Guid.Empty && id != null) {
			var tileSet = _project.TileSets.FirstOrDefault(tileSet => tileSet.Id == id);
			if (tileSet != null) {
				var tileScroll = new ScrollContainer {
					VerticalScrollMode = ScrollContainer.ScrollMode.Auto,
					HorizontalScrollMode = ScrollContainer.ScrollMode.ShowNever,
					SizeFlagsVertical = SizeFlags.ExpandFill,
					CustomMinimumSize = new Vector2(0, 100)
				};
				AreaTiles.AddChild(tileScroll);
				TileSelectorPanel tileSelector;
				tileScroll.AddChild(tileSelector = new TileSelectorPanel(tileSet.TileSet) {
					SizeFlagsHorizontal = SizeFlags.ExpandFill
				});
				tileSelector.TileSelected += TileSelector_OnTileSelected;
			}
		}
		AreaTiles.Visible = id != Guid.Empty && id != null;
	}

	private void TileCollectionEditor_OnTileSelected(TileInfo? obj) {
		TileDetails.Proxy = obj;
		SelectedTile = obj;
	}

	private void TileSelector_OnTileSelected(TileInfo? obj) {
		SelectedTile = obj;
	}

	public TileInfo? SelectedTile { get; private set; }

	public TileMapLayerInfo? SelectedLayer => _tileLayerEditor.SelectedLayer;

	public void LoadArea(AreaInfo? value) {
		_tileLayerEditor.LoadLayers(value?.TileMapLayers);
		TileSetSelector.LoadArea(value);
	}
}