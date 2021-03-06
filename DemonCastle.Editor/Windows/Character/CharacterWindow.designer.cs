using DemonCastle.Editor.Windows.Character.Animations;
using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character {
	public partial class CharacterWindow {
		protected PropertyCollection Properties { get; }
		public CharacterWindow(CharacterInfo characterInfo) {
			WindowTitle = $"Character - {characterInfo.FileName}";
			RectSize = new Vector2(600, 300);
			RectMinSize = RectSize;

			AddChild(Properties = new PropertyCollection {
				MarginLeft = 5,
				MarginTop = 5,
				MarginRight = 205,
				MarginBottom = -5,
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
				RectPosition = new Vector2(210, 5),
				MarginBottom = -5,
				AnchorBottom = 1
			});
			
			AddChild(new AnimationArea(characterInfo) {
				RectPosition = new Vector2(215, 5),
				MarginRight = -5,
				MarginBottom = -5,
				AnchorRight = 1,
				AnchorBottom = 1
			});
		}
	}
}