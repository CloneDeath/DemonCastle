using DemonCastle.Editor.Editors.Monster;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class MonsterEditorFileType : MonsterFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.MonsterIcon;
	public object CreateFileInstance(string name) => new MonsterFile {
		Name = name
	};

	public Control GetEditor(ProjectResources resources, ProjectInfo project, FileNavigator file) => new MonsterEditor(resources, project, resources.GetMonster(file));
}