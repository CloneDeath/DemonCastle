using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class TextFileWindow {
		protected TextEdit TextEdit { get; }
		
		public TextFileWindow(TextInfo textInfo) {
			WindowTitle = $"Text - {textInfo.FileName}";
			Size = new Vector2(300, 300);
			CustomMinimumSize = Size;
			
			AddChild(TextEdit = new TextEdit {
				AnchorRight = 1,
				AnchorBottom = 1,
				OffsetLeft = 5,
				OffsetTop = 5,
				OffsetBottom = -5,
				OffsetRight = -5,
				Text = textInfo.Contents
			});
		}
	}
}