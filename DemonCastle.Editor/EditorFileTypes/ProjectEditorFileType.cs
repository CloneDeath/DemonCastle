using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Project;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class ProjectEditorFileType : ProjectFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.ProjectIcon;

	public object CreateFileInstance(string name) => new ProjectFile {
		Name = name
	};

	public BaseEditor GetEditor(ProjectInfo project, FileNavigator file) => new ProjectEditor(file.ToProjectInfo());
}