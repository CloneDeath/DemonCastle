using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas; 

public partial class SpriteAtlasTextureEditor : ScrollContainer {
	protected TextureRect TextureRect { get; }
	
	public SpriteAtlasTextureEditor(SpriteAtlasInfo spriteAtlasInfo) {
		AddChild(TextureRect = new TextureRect {
			Texture = spriteAtlasInfo.Texture
		});

		foreach (var dataInfo in spriteAtlasInfo.SpriteData) {
			AddChild(new SpriteAtlasArea(dataInfo));
		}
	}
}