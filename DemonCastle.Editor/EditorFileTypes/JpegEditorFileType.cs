using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Icons;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class JpegEditorFileType : JpegFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.TextureIcon;

	public object CreateFileInstance(string name) => string.Empty;

	public Control GetEditor(ProjectResources resources, ProjectInfo project, FileNavigator file) => new ImageEditor(resources.GetTexture(file));
}