using DemonCastle.Editor.Editors.SpriteGrid;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class SpriteGridEditorFileType : SpriteGridFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.SpriteGridIcon;
	public object CreateFileInstance(string name) => new SpriteGridFile();
	public Control GetEditor(ProjectResources resources, ProjectInfo project, FileNavigator file) => new SpriteGridEditor(resources.GetSpriteGrid(file));
}