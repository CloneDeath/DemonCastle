using System.Linq;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels; 

public class SourceMetadata {
	private readonly TileSetAtlasSource _source;
	public readonly int SourceId;

	public SourceMetadata(TileSetAtlasSource source, int sourceId) {
		_source = source;
		SourceId = sourceId;
	}

	public void CreateTile(TileInfo tileInfo) {
		_source.CreateTile(tileInfo.AtlasCoords, tileInfo.AtlasSize);
		var tile = _source.GetTileData(tileInfo.AtlasCoords, 0);
		tile.FlipH = tileInfo.FlipHorizontal;
		
		// TileSetName(tileInfo.Index, tileInfo.Name);
		// TileSetTexture(tileInfo.Index, tileInfo.Texture);
		// TileSetRegion(tileInfo.Index, tileInfo.Region);
		if (tileInfo.Collision.Any()) {
			tile.AddCollisionPolygon((int)CollisionLayers.World);
			tile.SetCollisionPolygonPoints((int)CollisionLayers.World, 0, tileInfo.Collision);
		}
	}
}