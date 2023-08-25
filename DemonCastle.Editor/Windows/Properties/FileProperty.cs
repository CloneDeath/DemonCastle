using DemonCastle.Editor.Properties;
using Godot;
using Path = System.IO.Path;

namespace DemonCastle.Editor.Windows.Properties {
	public partial class FileProperty : StringProperty {
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
			LoadButton.Pressed += OnClick;

			AddChild(OpenFileDialog = new FileDialog {
				Filters = new [] {
					"*.png; Portable Network Graphic",
					"*.dcsg; Demon Castle Sprite Grid"
				},
				FileMode = FileDialog.FileModeEnum.OpenFile,
				Exclusive = true,
				Access = FileDialog.AccessEnum.Filesystem,
				Size = new Vector2I(800, 600),
				Unresizable = false,
				Title = "RefCounted Image"
			});
			OpenFileDialog.FileSelected += FileSelected;
		}

		protected void OnClick() {
			var fullPath = Path.Combine(Directory, PropertyValue);
			var directory = Path.GetDirectoryName(fullPath);
			OpenFileDialog.CurrentDir = directory;
			OpenFileDialog.CurrentFile = fullPath;
			OpenFileDialog.Popup();
		}

		protected void FileSelected(string filePath) {
			PropertyValue = RelativePath.GetRelativePath(Directory, filePath);
		}
	}
}