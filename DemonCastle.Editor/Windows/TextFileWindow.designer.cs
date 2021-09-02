using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class TextFileWindow {
		protected TextEdit TextEdit { get; }
		
		public TextFileWindow(TextInfo textInfo) {
			WindowTitle = $"Text - {textInfo.FileName}";
			RectSize = new Vector2(300, 300);
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