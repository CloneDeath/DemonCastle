using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class TextEditorFileType : TextFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.TextFileIcon;
	public object CreateFileInstance() => string.Empty;
	public BaseEditor GetEditor(FileNavigator file) => new TextFileEditor(file.ToTextInfo());
}