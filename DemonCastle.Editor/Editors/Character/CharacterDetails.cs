using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Character;

public partial class CharacterDetails : PropertyCollection {
	public CharacterDetails(CharacterInfo character) {
		Name = nameof(CharacterDetails);

		AddString("Name", character, x => x.Name);
		AddFloat("Walk Speed", character, x => x.WalkSpeed);
		AddFloat("Jump Height", character, x => x.JumpHeight);
		AddFloat("Gravity", character, x => x.Gravity);
		AddFloat("Width", character, x => x.Width);
		AddFloat("Height", character, x => x.Height);
		AddAnimationName("Idle Animation", character, x => x.IdleAnimation, character.Animations);
		AddAnimationName("Walk Animation", character, x => x.WalkAnimation, character.Animations);
		AddAnimationName("Jump Animation", character, x => x.JumpAnimation, character.Animations);
		AddAnimationName("Crouch Animation", character, x => x.CrouchAnimation, character.Animations);
		AddAnimationName("Stairs Up Animation", character, x => x.StairsUpAnimation, character.Animations);
		AddAnimationName("Stairs Down Animation", character, x => x.StairsDownAnimation, character.Animations);
		AddAnimationName("Stand Attack Animation", character, x => x.StandAttackAnimation, character.Animations);
		AddAnimationName("Jump Attack Animation", character, x => x.JumpAttackAnimation, character.Animations);
		AddAnimationName("Stairs Up Attack Animation", character, x => x.StairsUpAttackAnimation, character.Animations);
		AddAnimationName("Stairs Down Attack Animation", character, x => x.StairsDownAttackAnimation, character.Animations);
	}
}