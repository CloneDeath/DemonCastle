using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Item;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class ItemEditorFileType : ItemFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.ItemIcon;
	public object CreateFileInstance(string name) => new ItemFile {
		Name = name
	};

	public BaseEditor GetEditor(ProjectInfo project, FileNavigator file) => new ItemEditor(project, file.ToItemInfo());
}