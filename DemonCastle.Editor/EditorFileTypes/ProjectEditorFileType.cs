using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;
using ProjectEditor = DemonCastle.Editor.Editors.Project.ProjectEditor;

namespace DemonCastle.Editor.EditorFileTypes;

public class ProjectEditorFileType : ProjectFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.ProjectIcon;

	public object CreateFileInstance() => new ProjectFile {
		Name = "project"
	};

	public BaseEditor GetEditor(ProjectInfo project, FileNavigator file) => new ProjectEditor(file.ToProjectInfo());
}