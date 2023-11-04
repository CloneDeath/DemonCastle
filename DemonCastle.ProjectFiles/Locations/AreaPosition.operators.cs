using Godot;

namespace DemonCastle.ProjectFiles.Locations;

public partial class AreaPosition {
	public static AreaPosition operator +(AreaPosition left, Vector2I right) {
		return new AreaPosition(left.AreaIndex + right, left._areaScaleInTiles, left._tileScaleInPixels);
	}
}