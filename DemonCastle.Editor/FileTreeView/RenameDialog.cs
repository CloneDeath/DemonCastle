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
			MinSize += new Vector2I(0, 10);

			AboutToPopup += OnAboutToShow;
			
			AddChild(LineEdit = new LineEdit());
			LineEdit.GuiInput += OnLineEditGuiInput;
		}

		protected void OnAboutToShow() {
			LineEdit.SelectAll();
		}

		public void FocusLineEdit() {
			LineEdit.GrabFocus();
		}

		protected void OnLineEditGuiInput(InputEvent input) {
			if (!input.IsActionPressed(InputActions.EditorSubmit)) return;
			
			EmitSignal(AcceptDialog.SignalName.Confirmed);
			Hide();
		}
	}
}