using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors;

public partial class TextFileEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.TextFileIcon;
	public override string TabText { get; }

	protected TextEdit TextEdit { get; }

	public TextFileEditor(TextInfo textInfo) {
		Name = nameof(TextFileEditor);
		TabText = textInfo.FileName;

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