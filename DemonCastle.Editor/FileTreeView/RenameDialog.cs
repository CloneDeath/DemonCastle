using Godot;

namespace DemonCastle.Editor.FileTreeView;

public partial class RenameDialog : ConfirmationDialog {
	protected LineEdit LineEdit { get; }

	public string Text {
		get => LineEdit.Text;
		set => LineEdit.Text = value;
	}

	public RenameDialog() {
		Name = nameof(RenameDialog);
		Title = "Rename";
		Exclusive = true;
		MinSize += new Vector2I(0, 30);

		var vbox = new VBoxContainer();
		AddChild(vbox);
		vbox.AddChild(new Label {
			Text = "Enter a new name:"
		});
		vbox.AddChild(LineEdit = new LineEdit());
		RegisterTextEnter(LineEdit);
	}

	public void SelectAll() {
		LineEdit.SelectAll();
	}

	public void Select(int from = 0, int to = -1) {
		LineEdit.Select(from, to);
	}

	public void FocusLineEdit() {
		LineEdit.GrabFocus();
	}
}