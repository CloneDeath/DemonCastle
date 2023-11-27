using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Character;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class CharacterEditorFileType : CharacterFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.CharacterIcon;
	public object CreateFileInstance() => new CharacterFile {
		Name = "character"
	};

	public BaseEditor GetEditor(FileNavigator file) => new CharacterEditor(file.ToCharacterInfo());
}