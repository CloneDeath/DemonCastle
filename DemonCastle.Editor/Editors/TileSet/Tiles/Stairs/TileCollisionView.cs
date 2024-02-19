using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.TileSet.Tiles.Collision;

namespace DemonCastle.Editor.Editors.TileSet.Tiles.Stairs;

public partial class TileStairView : NamedPropertyCollection {
	public TileStairView(TileProxy tile) {
		Name = nameof(TileCollisionView);
		DisplayName = "Stairs";

		AddBoolean("Enabled", tile.Stairs, t => t.Enabled);
		AddVector2("Start", tile.Stairs, t => t.Start);
		AddVector2("End", tile.Stairs, t => t.End);
	}
}