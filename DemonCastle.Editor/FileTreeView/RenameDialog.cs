using DemonCastle.Game;
using Godot;

namespace DemonCastle.Editor.FileTreeView {
	public partial class RenameDialog : ConfirmationDialog {
		protected LineEdit LineEdit { get; }

		public string Text {
			get => LineEdit.Text;
			set => LineEdit.Text = value;
		}
		
		public RenameDialog() {
			Name = nameof(RenameDialog);
			DialogText = "Enter a new name:";
			Exclusive = true;
			CustomMinimumSize += new Vector2(0, 10);

			Connect("about_to_show", new Callable(this, nameof(OnAboutToShow)));
			
			AddChild(LineEdit = new LineEdit());
			LineEdit.Connect("gui_input", new Callable(this, nameof(OnLineEditGuiInput)));
		}

		protected void OnAboutToShow() {
			LineEdit.SelectAll();
		}

		public void FocusLineEdit() {
			LineEdit.GrabFocus();
		}

		protected void OnLineEditGuiInput(InputEvent input) {
			if (!input.IsActionPressed(InputActions.EditorSubmit)) return;
			
			EmitSignal("confirmed");
			Hide();
		}
	}
}