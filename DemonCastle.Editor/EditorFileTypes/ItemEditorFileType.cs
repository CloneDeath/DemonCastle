using DemonCastle.Editor.Editors.Item;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.Navigation;
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

	public Control GetEditor(ProjectResources resources, ProjectInfo project, FileNavigator file) => new ItemEditor(resources, project, resources.GetItem(file));
}