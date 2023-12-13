using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools;

public partial class MonsterToolsPanel : VBoxContainer {
	public MonsterToolsPanel() {
		Name = nameof(MonsterToolsPanel);

		AddChild(new Label { Text = "Monster Tools"});
	}
}