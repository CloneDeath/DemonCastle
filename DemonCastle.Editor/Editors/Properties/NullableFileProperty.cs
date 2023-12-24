using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles;
using Godot;
using Path = System.IO.Path;

namespace DemonCastle.Editor.Editors.Properties;

public partial class NullableFileProperty : NullableStringProperty {
	protected string Directory { get; }
	protected Button LoadButton { get; }
	protected Button ClearButton { get; }
	protected FileDialog OpenFileDialog { get; }

	public event Action<string>? FileSelected;

	public NullableFileProperty(IPropertyBinding<string?> binding, string directory, IEnumerable<IFileType> fileTypes)
		: base(binding) {
		Directory = directory;

		LineEdit.Editable = false;

		AddChild(ClearButton = new Button {
			Text = "x"
		});
		ClearButton.Pressed += ClearButton_OnClick;

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

	protected void ClearButton_OnClick() {
		PropertyValue = string.Empty;
		FileSelected?.Invoke(string.Empty);
		Binding.Set(PropertyValue);
	}

	protected void OnClick() {
		var fullPath = string.Empty;
		var directory = Directory;
		if (PropertyValue != null) {
			fullPath = Path.Combine(Directory, PropertyValue);
			directory = Path.GetDirectoryName(fullPath);
		}
		OpenFileDialog.CurrentFile = fullPath;
		OpenFileDialog.CurrentDir = directory;
		OpenFileDialog.Popup();
	}

	protected void OnFileSelected(string filePath) {
		PropertyValue = RelativePath.GetRelativePath(Directory, filePath);
		FileSelected?.Invoke(filePath);
		Binding.Set(PropertyValue);
	}

	public override void Enable() {
		base.Enable();
		ClearButton.Disabled = false;
		LoadButton.Disabled = false;
		LineEdit.Editable = false;
	}

	public override void Disable() {
		base.Disable();
		ClearButton.Disabled = true;
		LoadButton.Disabled = true;
		LineEdit.Editable = false;
	}
}