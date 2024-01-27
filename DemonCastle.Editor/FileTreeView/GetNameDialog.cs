using System.Threading.Tasks;
using Godot;

namespace DemonCastle.Editor.FileTreeView;

public partial class GetNameDialog : ConfirmationDialog {
	protected LineEdit LineEdit { get; }

	public string Text {
		get => LineEdit.Text;
		set => LineEdit.Text = value;
	}

	public GetNameDialog() {
		Name = nameof(RenameDialog);
		Title = "New File/Directory";
		Exclusive = true;
		MinSize += new Vector2I(0, 30);

		Confirmed += OnConfirmed;
		Canceled += OnCanceled;

		var vbox = new VBoxContainer();
		AddChild(vbox);
		vbox.AddChild(new Label {
			Text = "Name:"
		});
		vbox.AddChild(LineEdit = new LineEdit());
		RegisterTextEnter(LineEdit);
	}

	private void SelectAll() {
		LineEdit.SelectAll();
	}

	private void Select(int from = 0, int to = -1) {
		LineEdit.Select(from, to);
	}

	private void FocusLineEdit() {
		LineEdit.GrabFocus();
	}

	private TaskCompletionSource<string?>? _taskCompletionSource;

	public Task<string?> GetName() {
		_taskCompletionSource = new TaskCompletionSource<string?>();
		PopupCentered();
		FocusLineEdit();
		return _taskCompletionSource.Task;
	}

	private void OnConfirmed() => _taskCompletionSource?.SetResult(LineEdit.Text);
	private void OnCanceled() => _taskCompletionSource?.SetResult(null);
}