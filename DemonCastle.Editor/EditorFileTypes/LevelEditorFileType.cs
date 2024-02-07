using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Level;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class LevelEditorFileType : LevelFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.LevelIcon;
	public object CreateFileInstance(string name) => new LevelFile {
		Name = name
	};

	public BaseEditor GetEditor(ProjectResources resources, ProjectInfo project, FileNavigator file) => new LevelEditor(resources, resources.GetLevel(file));
}