using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Level;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class LevelEditorFileType : LevelFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.LevelIcon;
	public object CreateFileInstance(string name) => new LevelFile {
		Name = name
	};

	public BaseEditor GetEditor(ProjectInfo project, FileNavigator file) => new LevelEditor(project, file.ToLevelInfo());
}