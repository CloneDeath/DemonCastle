using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Character;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class CharacterEditorFileType : CharacterFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.CharacterIcon;
	public object CreateFileInstance() => new CharacterFile {
		Name = "character"
	};

	public BaseEditor GetEditor(ProjectInfo project, FileNavigator file) => new CharacterEditor(file.ToCharacterInfo());
}