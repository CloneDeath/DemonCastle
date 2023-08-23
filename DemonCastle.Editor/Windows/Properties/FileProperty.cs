using DemonCastle.Editor.Properties;
using Godot;
using Path3D = System.IO.Path3D;

namespace DemonCastle.Editor.Windows.Properties {
	public partial class FileProperty : StringProperty {
		protected string DirAccess { get; }
		protected Button LoadButton { get; }
		protected FileDialog OpenFileDialog { get; }
		public FileProperty(IPropertyBinding<string> binding, string directory)
			: base(binding) {
			DirAccess = directory;

			LineEdit.Editable = false;
			
			AddChild(LoadButton = new Button {
				Text = "..."
			});
			LoadButton.Connect("pressed", new Callable(this, nameof(OnClick)));

			AddChild(OpenFileDialog = new FileDialog {
				Filters = new [] {
					"*.png; Portable Network Graphic",
					"*.dcsg; Demon Castle Sprite2D Grid"
				},
				Mode = FileDialog.ModeEnum.OpenFile,
				Exclusive = true,
				Access = FileDialog.AccessEnum.Filesystem,
				Size = new Vector2(800, 600),
				Resizable = true,
				WindowTitle = "RefCounted Image"
			});
			OpenFileDialog.Connect("file_selected", new Callable(this, nameof(FileSelected)));
		}

		protected void OnClick() {
			var fullPath = Path3D.Combine(DirAccess, PropertyValue);
			var directory = Path3D.GetDirectoryName(fullPath);
			OpenFileDialog.CurrentDir = directory;
			OpenFileDialog.CurrentFile = fullPath;
			OpenFileDialog.Popup_();
		}

		protected void FileSelected(string filePath) {
			PropertyValue = RelativePath.GetRelativePath(DirAccess, filePath);
		}
	}
}