using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor.Frame;

public partial class CharacterFrameDetails : PropertyCollection {
	private CharacterFrameInfo Frame { get; }

	protected SpriteReferenceProperty SpriteReference { get; }

	public CharacterFrameDetails(CharacterFrameInfo frame) {
		Frame = frame;
		Name = nameof(CharacterFrameDetails);

		var source = AddFile("Source", frame, frame.Directory, f => f.SourceFile, FileType.SpriteSources);
		source.FileSelected += Source_OnFileSelected;
		SpriteReference = AddSpriteReference("Sprite", frame, f => f.SpriteId, frame.SpriteDefinitions);
		AddFloat("Duration", frame, f => f.Duration);
		AddBoolean("Weapon Enabled", frame, f => f.WeaponEnabled);
		AddString("Weapon Animation", frame, f => f.WeaponAnimation);
		AddVector2I("Weapon Position", frame, f => f.WeaponPosition);
	}

	private void Source_OnFileSelected(string file) {
		SpriteReference.LoadOptions(Frame.SpriteDefinitions);
	}
}