using DemonCastle.Editor.Editors.SpriteAtlas;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class SpriteAtlasEditorFileType : SpriteAtlasFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.SpriteAtlasIcon;
	public object CreateFileInstance(string name) => new SpriteAtlasFile();
	public Control GetEditor(ProjectResources resources, ProjectInfo project, FileNavigator file) => new SpriteAtlasEditor(resources.GetSpriteAtlas(file));
}