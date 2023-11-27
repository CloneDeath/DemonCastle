using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Weapon;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class WeaponEditorFileType : WeaponFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.WeaponIcon;
	public object CreateFileInstance() => new WeaponFile {
		Name = "weapon"
	};

	public BaseEditor GetEditor(FileNavigator file) => new WeaponEditor(file.ToWeaponInfo());
}