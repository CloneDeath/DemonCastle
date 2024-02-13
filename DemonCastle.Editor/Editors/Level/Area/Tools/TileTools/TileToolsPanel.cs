using System;
using DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.Layers;
using DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.TileSets;
using DemonCastle.Editor.Editors.TileSet.Tiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class TileToolsPanel : VBoxContainer {
	private readonly ProjectResources _resources;
	public event Action<int>? SelectedLayerIndexChanged;

	protected LevelInfo Level { get; }

	protected readonly TileLayerEditor TileLayerEditor;
	protected TileSetSelector TileSetSelector { get; }

	protected VBoxContainer LevelTiles { get; }
	protected TileInfoCollectionEditor TileCollectionEditor { get; }
	protected LevelTileDetails TileDetails { get; }

	protected VBoxContainer AreaTiles { get; }

	public TileToolsPanel(ProjectResources resources, LevelInfo level) {
		_resources = resources;
		Name = nameof(TileToolsPanel);
		Level = level;

		AddChild(TileLayerEditor = new TileLayerEditor());
		TileLayerEditor.SelectedLayerIndexChanged += index => SelectedLayerIndexChanged?.Invoke(index);
		AddChild(TileSetSelector = new TileSetSelector(resources, level.Directory));
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

	private void TileSetSelector_OnTileSetIdSelected(Guid? tileSetId) {
		LevelTiles.Visible = tileSetId == Guid.Empty;
		foreach (var child in AreaTiles.GetChildren()) {
			child.QueueFree();
		}
		if (tileSetId != Guid.Empty && tileSetId != null) {
			var tileSet = _resources.GetTileSet(tileSetId.Value);
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
		AreaTiles.Visible = tileSetId != Guid.Empty && tileSetId != null;
	}

	private void TileCollectionEditor_OnTileSelected(TileInfo? obj) {
		TileDetails.Proxy = obj;
		SelectedTile = obj;
	}

	private void TileSelector_OnTileSelected(TileInfo? obj) {
		SelectedTile = obj;
	}

	public TileInfo? SelectedTile { get; private set; }

	public TileMapLayerInfo? SelectedLayer => TileLayerEditor.SelectedLayer;

	public void LoadArea(AreaInfo? value) {
		TileLayerEditor.LoadLayers(value?.TileMapLayers);
		TileSetSelector.LoadArea(value);
	}
}