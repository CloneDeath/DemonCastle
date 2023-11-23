using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class PngEditorFileType : PngFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.TextureIcon;

	public object CreateFileInstance() => string.Empty;

	public BaseEditor GetEditor(FileNavigator file) => new ImageEditor(file);
}