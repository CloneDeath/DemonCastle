using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class ProjectEditorFileType : ProjectFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.ProjectIcon;

	public object CreateFileInstance() => new ProjectFile {
		Name = "project"
	};

	public BaseEditor GetEditor(FileNavigator file) => new ProjectEditor(file.ToProjectInfo());
}