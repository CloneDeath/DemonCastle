using DemonCastle.Editor.Editors.Character.Animations;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Character;

public partial class CharacterEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.CharacterIcon;
	public override string TabText { get; }

	protected PropertyCollection Properties { get; }

	public CharacterEditor(CharacterInfo characterInfo) {
		Name = nameof(CharacterEditor);
		TabText = characterInfo.FileName;
		CustomMinimumSize = new Vector2I(600, 300);

		AddChild(Properties = new PropertyCollection {
			OffsetLeft = 5,
			OffsetTop = 5,
			OffsetRight = 205,
			OffsetBottom = -5
		});
		Properties.SetAnchorsPreset(LayoutPreset.LeftWide, true);
		Properties.AddString("Name", characterInfo, x => x.Name);
		Properties.AddFloat("Walk Speed", characterInfo, x => x.WalkSpeed);
		Properties.AddFloat("Jump Height", characterInfo, x => x.JumpHeight);
		Properties.AddFloat("Gravity", characterInfo, x => x.Gravity);
		Properties.AddFloat("Width", characterInfo, x => x.Width);
		Properties.AddFloat("Height", characterInfo, x => x.Height);
		Properties.AddString("Idle Animation", characterInfo, x => x.IdleAnimation);
		Properties.AddString("Walk Animation", characterInfo, x => x.WalkAnimation);
		Properties.AddString("Jump Animation", characterInfo, x => x.JumpAnimation);

		AddChild(new VSeparator {
			Name = nameof(VSeparator),
			Position = new Vector2(210, 5),
			OffsetBottom = -5,
			AnchorBottom = 1
		});

		AddChild(new AnimationArea(characterInfo) {
			Position = new Vector2(215, 5),
			OffsetRight = -5,
			OffsetBottom = -5,
			AnchorRight = 1,
			AnchorBottom = 1
		});
	}
}