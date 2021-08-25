using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class SpriteAtlasWindow {
		public SpriteAtlasWindow(SpriteAtlasInfo spriteAtlasInfo) {
			WindowTitle = spriteAtlasInfo.FileName;
			RectSize = new Vector2(300, 300);
		}
	}
}