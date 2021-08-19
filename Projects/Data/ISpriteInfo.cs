using Godot;

namespace DemonCastle.Projects.Data {
	public interface ISpriteInfo {
		SpriteInfoNode GetSprite(string spriteName);
		Rect2 GetRegion(string spriteName);
		Texture Texture { get; }
	}
}