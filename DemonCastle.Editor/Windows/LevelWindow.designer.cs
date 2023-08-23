using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class LevelWindow {
		public LevelWindow(LevelInfo levelInfo) {
			Title = $"Level - {levelInfo.FileName}";
			Size = new Vector2I(300, 300);
			MinSize = Size;
		}
	}
}