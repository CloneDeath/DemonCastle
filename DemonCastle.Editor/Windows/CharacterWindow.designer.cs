using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class CharacterWindow {
		public CharacterWindow(CharacterInfo characterInfo) {
			WindowTitle = $"Character - {characterInfo.FileName}";
			RectSize = new Vector2(300, 300);

			var charName = new HBoxContainer {
				MarginLeft = 5,
				MarginTop = 5
			};
			AddChild(charName);
			charName.AddChild(new Label {
				Text = "Name: ",
			});
			charName.AddChild(new TextEdit {
				Text = characterInfo.Name,
				RectMinSize = new Vector2(100, 20)
			});
		}
	}
}