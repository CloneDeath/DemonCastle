using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Level;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class LevelEditorFileType : LevelFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.LevelIcon;
	public object CreateFileInstance() => new LevelFile {
		Name = "level"
	};

	public BaseEditor GetEditor(FileNavigator file) => new LevelEditor(file.ToLevelInfo());
}