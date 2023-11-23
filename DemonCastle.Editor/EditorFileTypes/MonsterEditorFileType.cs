using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.FileTypes;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class MonsterEditorFileType : MonsterFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.MonsterIcon;
	public object CreateFileInstance() => new MonsterFile {
		Name = "monster"
	};
}