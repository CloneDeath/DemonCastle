using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools;

public partial class MonsterToolsPanel : VBoxContainer {
	public MonsterToolsPanel(ProjectInfo project) {
		Name = nameof(MonsterToolsPanel);

		AddChild(new Label { Text = "Monster Tools"});
		foreach (var monster in project.Monsters) {
			AddChild(new Label { Text = monster.Name});
		}
	}
}