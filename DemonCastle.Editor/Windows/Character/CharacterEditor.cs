using DemonCastle.Editor.Windows.Character.Animations;
using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character; 

public partial class CharacterEditor : Control {
	protected PropertyCollection Properties { get; }
	
	public CharacterEditor(CharacterInfo characterInfo) {
		Name = $"Character - {characterInfo.FileName}";
		CustomMinimumSize = new Vector2I(600, 300);

		AddChild(Properties = new PropertyCollection {
			OffsetLeft = 5,
			OffsetTop = 5,
			OffsetRight = 205,
			OffsetBottom = -5,
			AnchorBottom = 1
		});
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