using DemonCastle.Editor.Properties;
using Godot;
using Path = System.IO.Path;

namespace DemonCastle.Editor.Windows.Properties {
	public class FileProperty : StringProperty {
		protected string Directory { get; }
		protected Button LoadButton { get; }
		protected FileDialog OpenFileDialog { get; }
		public FileProperty(IPropertyBinding<string> binding, string directory)
			: base(binding) {
			Directory = directory;

			LineEdit.Editable = false;
			
			AddChild(LoadButton = new Button {
				Text = "..."
			});
			LoadButton.Connect("pressed", this, nameof(OnClick));

			AddChild(OpenFileDialog = new FileDialog {
				Filters = new [] {
					"*.png; Portable Network Graphic",
					"*.dcsg; Demon Castle Sprite Grid"
				},
				Mode = FileDialog.ModeEnum.OpenFile,
				PopupExclusive = true,
				Access = FileDialog.AccessEnum.Filesystem,
				RectSize = new Vector2(800, 600),
				Resizable = true,
				WindowTitle = "Reference Image"
			});
			OpenFileDialog.Connect("file_selected", this, nameof(FileSelected));
		}

		protected void OnClick() {
			var fullPath = Path.Combine(Directory, PropertyValue);
			var directory = Path.GetDirectoryName(fullPath);
			OpenFileDialog.CurrentDir = directory;
			OpenFileDialog.CurrentFile = fullPath;
			OpenFileDialog.Popup_();
		}

		protected void FileSelected(string filePath) {
			PropertyValue = RelativePath.GetRelativePath(Directory, filePath);
		}
	}
}