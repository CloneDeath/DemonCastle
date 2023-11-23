using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.FileTypes;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class SpriteAtlasEditorFileType : SpriteAtlasFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.SpriteAtlasIcon;
	public object CreateFileInstance() => new SpriteAtlasFile();
}