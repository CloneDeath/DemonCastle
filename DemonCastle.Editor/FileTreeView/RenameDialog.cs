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
			Title = "Rename";
			Exclusive = true;
			MinSize += new Vector2I(0, 30);

			AboutToPopup += OnAboutToShow;

			var vbox = new VBoxContainer();
			AddChild(vbox);
			vbox.AddChild(new Label {
				Text = "Enter a new name:"
			});
			vbox.AddChild(LineEdit = new LineEdit());
			RegisterTextEnter(LineEdit);
		}

		protected void OnAboutToShow() {
			LineEdit.SelectAll();
		}

		public void FocusLineEdit() {
			LineEdit.GrabFocus();
		}
	}
}