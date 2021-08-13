using Godot;

namespace DemonCastle.Projects.Data {
	public class SpriteInfoNode : Sprite {
		public SpriteInfoNode(Texture texture, Rect2 region, bool flipH) {
			Texture = texture;
			RegionEnabled = true;
			RegionRect = region;
			FlipH = flipH;
		}
	}
}