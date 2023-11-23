using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.FileTypes;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class LevelEditorFileType : LevelFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.LevelIcon;
	public object CreateFileInstance() => new LevelFile {
		Name = "level"
	};
}