using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.SpriteAtlas;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class SpriteAtlasEditorFileType : SpriteAtlasFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.SpriteAtlasIcon;
	public object CreateFileInstance() => new SpriteAtlasFile();
	public BaseEditor GetEditor(FileNavigator file) => new SpriteAtlasEditor(file.ToSpriteAtlasInfo());
}