using DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.TileSets;
using DemonCastle.Editor.Editors.TileSet.Tiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class TileToolsPanel : VBoxContainer {
	protected LevelInfo Level { get; }

	protected readonly Layers.TileLayerEditor _tileLayerEditor;
	protected TileSetSelector TileSetSelector { get; }
	protected TileInfoCollectionEditor TileCollectionEditor { get; }
	protected LevelTileDetails TileDetails { get; }

	public TileToolsPanel(LevelInfo level) {
		Name = nameof(TileToolsPanel);
		Level = level;

		AddChild(_tileLayerEditor = new Layers.TileLayerEditor());
		AddChild(TileSetSelector = new TileSetSelector());
		AddChild(TileCollectionEditor = new TileInfoCollectionEditor(level.TileSet));
		TileCollectionEditor.TileSelected += TileCollectionEditor_OnTileSelected;
		AddChild(TileDetails = new LevelTileDetails(level.Directory));
	}

	private void TileCollectionEditor_OnTileSelected(TileInfo? obj) {
		TileDetails.Proxy = obj;
	}

	public TileInfo? SelectedTile => TileCollectionEditor.SelectedTile;
	public TileMapLayerInfo? SelectedLayer => _tileLayerEditor.SelectedLayer;

	public void LoadArea(AreaInfo? value) {
		_tileLayerEditor.LoadLayers(value?.TileMapLayers);
	}
}