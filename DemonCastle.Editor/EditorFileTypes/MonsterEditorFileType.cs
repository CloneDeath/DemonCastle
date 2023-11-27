using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Monster;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class MonsterEditorFileType : MonsterFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.MonsterIcon;
	public object CreateFileInstance() => new MonsterFile {
		Name = "monster"
	};

	public BaseEditor GetEditor(FileNavigator file) => new MonsterEditor(file.ToMonsterInfo());
}