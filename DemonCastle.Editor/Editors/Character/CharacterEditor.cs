using DemonCastle.Editor.Editors.Character.Animations;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Character;

public partial class CharacterEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.CharacterIcon;
	public override string TabText { get; }

	protected HSplitContainer SplitContainer { get; }

	protected PropertyCollection Properties { get; }

	public CharacterEditor(CharacterInfo characterInfo) {
		Name = nameof(CharacterEditor);
		TabText = characterInfo.FileName;
		CustomMinimumSize = new Vector2I(600, 300);

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", characterInfo, x => x.Name);
		Properties.AddFloat("Walk Speed", characterInfo, x => x.WalkSpeed);
		Properties.AddFloat("Jump Height", characterInfo, x => x.JumpHeight);
		Properties.AddFloat("Gravity", characterInfo, x => x.Gravity);
		Properties.AddFloat("Width", characterInfo, x => x.Width);
		Properties.AddFloat("Height", characterInfo, x => x.Height);
		Properties.AddAnimationName("Idle Animation", characterInfo, x => x.IdleAnimation, characterInfo.Animations);
		Properties.AddAnimationName("Walk Animation", characterInfo, x => x.WalkAnimation, characterInfo.Animations);
		Properties.AddAnimationName("Jump Animation", characterInfo, x => x.JumpAnimation, characterInfo.Animations);
		Properties.AddAnimationName("Crouch Animation", characterInfo, x => x.CrouchAnimation, characterInfo.Animations);
		Properties.AddAnimationName("Stairs Up Animation", characterInfo, x => x.StairsUpAnimation, characterInfo.Animations);
		Properties.AddAnimationName("Stairs Down Animation", characterInfo, x => x.StairsDownAnimation, characterInfo.Animations);

		SplitContainer.AddChild(new AnimationArea(characterInfo));
	}
}