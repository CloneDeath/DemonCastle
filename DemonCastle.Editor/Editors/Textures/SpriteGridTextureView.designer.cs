using DemonCastle.ProjectFiles.Projects.Data.Sprites;

namespace DemonCastle.Editor.Editors.Textures {
	public partial class SpriteGridTextureView {
		protected SpriteGridInfo SpriteGridInfo { get; }

		public SpriteGridTextureView(SpriteGridInfo spriteGridInfo) {
			SpriteGridInfo = spriteGridInfo;
		}
	}
}