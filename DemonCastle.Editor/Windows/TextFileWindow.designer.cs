using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class TextFileWindow {
		protected TextEdit TextEdit { get; }
		protected TextureRect ProgramIcon { get; }
		
		public TextFileWindow(TextInfo textInfo) {
			RectSize = new Vector2(300, 300);
			WindowTitle = textInfo.FileName;
			Resizable = true;

			AddChild(TextEdit = new TextEdit {
				AnchorRight = 1,
				AnchorBottom = 1,
				MarginLeft = 5,
				MarginTop = 5,
				MarginBottom = -5,
				MarginRight = -5,
				Text = textInfo.Contents
			});
		}
	}
}