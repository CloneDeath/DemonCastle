using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.SpriteGrid;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class SpriteGridEditorFileType : SpriteGridFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.SpriteGridIcon;
	public object CreateFileInstance() => new SpriteGridFile();
	public BaseEditor GetEditor(FileNavigator file) => new SpriteGridEditor(file.ToSpriteGridInfo());
}