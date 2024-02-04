using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;

public class TileMapLayerInfo : BaseInfo<TileMapLayerData> {
	public TileMapLayerInfo(IFileNavigator file, TileMapLayerData tileMapLayer, AreaInfo area) : base(file, tileMapLayer) {
		TileMap = new InfoList<TileMapInfo,TileMapData>(file, tileMapLayer.TileMap, data => new TileMapInfo(file, data, area));
	}

	public string Name {
		get => Data.Name;
		set => SaveField(ref Data.Name, value);
	}

	public int ZIndex {
		get => Data.ZIndex;
		set => SaveField(ref Data.ZIndex, value);
	}

	public ObservableCollectionInfo<TileMapInfo, TileMapData> TileMap { get; }
}