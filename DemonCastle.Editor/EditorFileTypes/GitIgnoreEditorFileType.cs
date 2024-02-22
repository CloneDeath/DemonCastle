using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Icons;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class GitIgnoreEditorFileType : GitIgnoreFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.GitIcon;
	public object CreateFileInstance(string name) => string.Empty;
	public BaseEditor GetEditor(ProjectResources resources, ProjectInfo project, FileNavigator file) => new TextFileEditor(resources.GetText(file));
}