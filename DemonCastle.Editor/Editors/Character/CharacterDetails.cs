using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles;
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
		AddFile("Default Weapon", character, character.Directory, x => x.DefaultWeapon, new[]{FileType.Weapon});
		AddAnimationReference("Idle Animation", character, x => x.IdleAnimation, character.Animations);
		AddAnimationReference("Walk Animation", character, x => x.WalkAnimation, character.Animations);
		AddAnimationReference("Jump Animation", character, x => x.JumpAnimation, character.Animations);
		AddAnimationReference("Crouch Animation", character, x => x.CrouchAnimation, character.Animations);
		AddAnimationReference("Stairs Up Animation", character, x => x.StairsUpAnimation, character.Animations);
		AddAnimationReference("Stairs Down Animation", character, x => x.StairsDownAnimation, character.Animations);
		AddAnimationReference("Stand Attack Animation", character, x => x.StandAttackAnimation, character.Animations);
		AddAnimationReference("Jump Attack Animation", character, x => x.JumpAttackAnimation, character.Animations);
		AddAnimationReference("Stairs Up Attack Animation", character, x => x.StairsUpAttackAnimation, character.Animations);
		AddAnimationReference("Stairs Down Attack Animation", character, x => x.StairsDownAttackAnimation, character.Animations);
	}
}