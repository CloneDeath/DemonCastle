using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors; 

public partial class TextFileEditor : Control {
	protected TextEdit TextEdit { get; }
		
	public TextFileEditor(TextInfo textInfo) {
		Name = $"Text - {textInfo.FileName}";
		CustomMinimumSize = new Vector2I(300, 300);
		
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