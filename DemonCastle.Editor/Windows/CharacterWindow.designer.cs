using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class CharacterWindow {
		protected PropertyCollection Properties { get; }
		public CharacterWindow(CharacterInfo characterInfo) {
			WindowTitle = $"Character - {characterInfo.FileName}";
			RectSize = new Vector2(300, 300);
			RectMinSize = RectSize;

			AddChild(Properties = new PropertyCollection {
				MarginLeft = 5,
				MarginTop = 5,
				MarginRight = -5,
				MarginBottom = -5,
				AnchorRight = 1,
				AnchorBottom = 1
			});
			Properties.AddString("Name", characterInfo, x => x.Name);
			Properties.AddFloat("Walk Speed", characterInfo, x => x.WalkSpeed);
			Properties.AddFloat("Jump Height", characterInfo, x => x.JumpHeight);
			Properties.AddFloat("Gravity", characterInfo, x => x.Gravity);
			
		}
	}
}