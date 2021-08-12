using Godot;

namespace DemonCastle.Projects.Data {
	public class SpriteInfoNode : Sprite {
		public SpriteInfoNode(Texture texture, Rect2 region) {
			Texture = texture;
			RegionEnabled = true;
			RegionRect = region;
		}
	}
}