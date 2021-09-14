using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class LevelWindow {
		public LevelWindow(LevelInfo levelInfo) {
			WindowTitle = $"Level - {levelInfo.FileName}";
			RectSize = new Vector2(300, 300);
			RectMinSize = RectSize;
		}
	}
}