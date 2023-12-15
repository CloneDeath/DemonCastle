using System.Linq;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools;

public partial class MonsterToolsPanel : VBoxContainer {
	public MonsterToolsPanel(ProjectInfo project) {
		Name = nameof(MonsterToolsPanel);

		AddChild(new Button { Text = "Add Monster"});
		AddChild(new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		AddChild(new Button { Text = "Remove Monster"});

		AddChild(new OptionButton());
		AddChild(new TextEdit());

		AddChild(new Label { Text = "Monster Tools"});
		foreach (var monster in project.Monsters) {
			var state = monster.States.FirstOrDefault(s => s.Id == monster.InitialState);
			var animation = monster.Animations.FirstOrDefault(a => a.Id == state?.Animation);
			AddChild(new SpriteDefinitionView( animation?.Frames.First().SpriteDefinition ?? new NullSpriteDefinition()));
			AddChild(new Label { Text = monster.Name});
		}
	}
}