using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors;

public partial class TextFileEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.File.TextFileIcon;
	public override string TabText { get; }
	private readonly TextInfo _textInfo;

	protected TextEdit TextEdit { get; }

	public TextFileEditor(TextInfo textInfo) {
		Name = nameof(TextFileEditor);
		TabText = textInfo.FileName;
		_textInfo = textInfo;

		AddChild(TextEdit = new TextEdit {
			Text = textInfo.Contents
		});
		TextEdit.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
		TextEdit.TextChanged += TextEdit_OnTextChanged;
	}

	private void TextEdit_OnTextChanged() {
		_textInfo.Contents = TextEdit.Text;
	}
}