using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.FileTypes;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class CharacterEditorFileType : CharacterFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.CharacterIcon;
	public object CreateFileInstance() => new CharacterFile {
		Name = "character"
	};
}