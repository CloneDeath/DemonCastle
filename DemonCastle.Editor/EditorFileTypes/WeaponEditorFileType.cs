using DemonCastle.Editor.Editors.Weapon;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class WeaponEditorFileType : WeaponFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.File.WeaponIcon;
	public object CreateFileInstance(string name) => new WeaponFile {
		Name = name
	};

	public Control GetEditor(ProjectResources resources, ProjectInfo project, FileNavigator file) => new WeaponEditor(resources.GetWeapon(file));
}