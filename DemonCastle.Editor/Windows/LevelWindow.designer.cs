using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class LevelWindow {
		public LevelWindow(LevelInfo spriteAtlasInfo) {
			WindowTitle = spriteAtlasInfo.FileName;
			RectSize = new Vector2(300, 300);
		}
	}
}