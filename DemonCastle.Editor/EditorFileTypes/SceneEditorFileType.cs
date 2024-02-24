using DemonCastle.Editor.Editors.Scene;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class SceneEditorFileType : SceneFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.SceneIcon;
	public object CreateFileInstance(string name) => new SceneFile {
		Name = name
	};

	public Control GetEditor(ProjectResources resources, ProjectInfo project, FileNavigator file) => new SceneEditor(project, resources.GetScene(file));
}