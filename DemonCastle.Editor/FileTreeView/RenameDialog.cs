using Godot;

namespace DemonCastle.Editor.FileTreeView {
	public class RenameDialog : ConfirmationDialog {
		protected LineEdit LineEdit { get; }

		public string Text {
			get => LineEdit.Text;
			set => LineEdit.Text = value;
		}
		
		public RenameDialog() {
			Name = nameof(RenameDialog);
			DialogText = "Enter a new name:";
			PopupExclusive = true;
			RectMinSize += new Vector2(0, 10);
			
			AddChild(LineEdit = new LineEdit());
			Connect("about_to_show", this, nameof(OnAboutToShow));
		}

		protected void OnAboutToShow() {
			LineEdit.GrabFocus();
			LineEdit.SelectAll();
		}
	}
}