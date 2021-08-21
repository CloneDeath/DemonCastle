using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites {
	public interface ISpriteInfo {
		SpriteInfoNode GetSprite(string spriteName);
		Rect2 GetRegion(string spriteName);
		Texture Texture { get; }
	}
}