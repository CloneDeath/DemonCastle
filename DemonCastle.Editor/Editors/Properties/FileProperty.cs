using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.FileTypes;
using Godot;
using Path = System.IO.Path;

namespace DemonCastle.Editor.Editors.Properties;

public partial class FileProperty : StringProperty {
	protected string Directory { get; }
	protected Button LoadButton { get; }
	protected FileDialog OpenFileDialog { get; }

	public event Action<string>? FileSelected;

	public FileProperty(IPropertyBinding<string> binding, string directory, IEnumerable<IFileTypeData> fileTypes)
		: base(binding) {
		Directory = directory;

		LineEdit.Editable = false;

		AddChild(LoadButton = new Button {
			Text = "..."
		});
		LoadButton.Pressed += OnClick;

		AddChild(OpenFileDialog = new FileDialog {
			Filters = fileTypes.Select(t => t.Filter).ToArray(),
			FileMode = FileDialog.FileModeEnum.OpenFile,
			Exclusive = true,
			Access = FileDialog.AccessEnum.Filesystem,
			Size = new Vector2I(800, 600),
			Unresizable = false,
			Title = "Select File"
		});
		OpenFileDialog.FileSelected += OnFileSelected;
	}

	protected void OnClick() {
		var fullPath = Path.Combine(Directory, PropertyValue);
		var directory = Path.GetDirectoryName(fullPath);
		OpenFileDialog.CurrentDir = directory;
		OpenFileDialog.CurrentFile = fullPath;
		OpenFileDialog.Popup();
	}

	protected void OnFileSelected(string filePath) {
		PropertyValue = RelativePath.GetRelativePath(Directory, filePath);
		FileSelected?.Invoke(filePath);
	}

	public override void Enable() {
		base.Enable();
		LoadButton.Disabled = false;
		LineEdit.Editable = false;
	}

	public override void Disable() {
		base.Disable();
		LoadButton.Disabled = true;
		LineEdit.Editable = false;
	}
}