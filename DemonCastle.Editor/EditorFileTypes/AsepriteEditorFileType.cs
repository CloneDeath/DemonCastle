using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class AsepriteEditorFileType : AsepriteFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.AsepriteIcon;
	public object CreateFileInstance() => string.Empty;
	public BaseEditor GetEditor(ProjectInfo project, FileNavigator file) => new TextFileEditor(file.ToTextInfo());
}