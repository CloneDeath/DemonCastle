using DemonCastle.Editor.Icons;
using DemonCastle.Game;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors;

public partial class TextFileEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.TextFileIcon;
	public override string TabText { get; }
	private readonly TextInfo _textInfo;

	protected TextEdit TextEdit { get; }

	public TextFileEditor(TextInfo textInfo) {
		Name = nameof(TextFileEditor);
		TabText = textInfo.FileName;
		_textInfo = textInfo;

		AddChild(TextEdit = new TextEdit {
			AnchorRight = 1,
			AnchorBottom = 1,
			OffsetLeft = 5,
			OffsetTop = 5,
			OffsetBottom = -5,
			OffsetRight = -5,
			Text = textInfo.Contents
		});
		TextEdit.TextChanged += TextEdit_OnTextChanged;
	}

	public override void _Input(InputEvent @event) {
		base._Input(@event);
		if (!@event.IsAction(InputActions.EditorSave)) return;

		_textInfo.Save();
		AcceptEvent();
	}

	private void TextEdit_OnTextChanged() {
		_textInfo.Contents = TextEdit.Text;
	}
}