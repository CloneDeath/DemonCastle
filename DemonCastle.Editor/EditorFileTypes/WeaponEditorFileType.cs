using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.FileTypes;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class WeaponEditorFileType : WeaponFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.WeaponIcon;
	public object CreateFileInstance() => new WeaponFile {
		Name = "weapon"
	};
}