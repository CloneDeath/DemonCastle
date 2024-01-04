using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Item;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class ItemEditorFileType : ItemFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.ItemIcon;
	public object CreateFileInstance() => new ItemFile {
		Name = "item"
	};

	public BaseEditor GetEditor(ProjectInfo project, FileNavigator file) => new ItemEditor(file.ToItemInfo());
}