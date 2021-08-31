using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Windows.Textures {
	public partial class SpriteGridTextureView {
		public SpriteGridTextureView(SpriteGridInfo spriteGridInfo)
			: base(spriteGridInfo.Texture) {
			
		}
	}
}