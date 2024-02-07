using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.TileSet;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class TileSetEditorFileType : TileSetFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.TileSetIcon;
	public object CreateFileInstance(string name) => new TileSetFile {
		Name = name
	};
	public BaseEditor GetEditor(ProjectResources resources, ProjectInfo project, FileNavigator file) => new TileSetEditor(resources, project, resources.GetTileSet(file));
}