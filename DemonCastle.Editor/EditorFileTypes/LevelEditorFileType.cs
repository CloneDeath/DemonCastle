using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Level;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class LevelEditorFileType : LevelFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.LevelIcon;
	public object CreateFileInstance() => new LevelFile {
		Name = "level"
	};

	public BaseEditor GetEditor(ProjectInfo project, FileNavigator file) => new LevelEditor(project, file.ToLevelInfo());
}