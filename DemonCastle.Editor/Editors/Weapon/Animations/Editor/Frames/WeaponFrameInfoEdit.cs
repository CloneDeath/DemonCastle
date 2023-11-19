using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor.Frames;

public partial class WeaponFrameInfoEdit : PropertyCollection {
	private  readonly WeaponFrameInfoProxy _proxy = new();

	protected SpriteReferenceProperty SpriteReference { get; }

	public WeaponFrameInfo? WeaponFrameInfo {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public WeaponFrameInfoEdit(WeaponInfo weapon) {
		Name = nameof(WeaponFrameInfoEdit);

		AddFloat("Duration", _proxy, p => p.Duration);
		var source = AddFile("Source", _proxy, weapon.Directory, p => p.SourceFile, FileType.SpriteSources);
		source.FileSelected += Source_OnFileSelected;
		SpriteReference = AddSpriteReference("Sprite", _proxy, p => p.SpriteId, _proxy.SpriteDefinitions);
	}

	private void Source_OnFileSelected(string file) {
		SpriteReference.LoadOptions(_proxy.SpriteDefinitions);
	}
}