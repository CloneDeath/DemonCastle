using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Monster;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class MonsterEditorFileType : MonsterFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.MonsterIcon;
	public object CreateFileInstance() => new MonsterFile {
		Name = "monster"
	};

	public BaseEditor GetEditor(ProjectInfo project, FileNavigator file) => new MonsterEditor(project, file.ToMonsterInfo());
}