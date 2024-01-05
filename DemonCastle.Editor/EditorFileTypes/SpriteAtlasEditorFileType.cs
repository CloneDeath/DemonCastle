using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.SpriteAtlas;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class SpriteAtlasEditorFileType : SpriteAtlasFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.SpriteAtlasIcon;
	public object CreateFileInstance() => new SpriteAtlasFile();
	public BaseEditor GetEditor(ProjectInfo project, FileNavigator file) => new SpriteAtlasEditor(file.ToSpriteAtlasInfo());
}