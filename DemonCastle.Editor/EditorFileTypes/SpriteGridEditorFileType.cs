using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.FileTypes;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class SpriteGridEditorFileType : SpriteGridFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.SpriteGridIcon;
	public object CreateFileInstance() => new SpriteGridFile();
}