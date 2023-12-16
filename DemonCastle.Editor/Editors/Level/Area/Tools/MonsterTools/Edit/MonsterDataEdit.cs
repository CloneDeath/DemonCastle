using System.Linq;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools.Edit;

public partial class MonsterDataEdit : PropertyCollection {
	public MonsterDataInfo? MonsterData { get; set; }

	public MonsterDataEdit(ProjectInfo project) {

		AddChild(new OptionButton());
		AddChild(new TextEdit());

		foreach (var monster in project.Monsters) {
			var state = monster.States.FirstOrDefault(s => s.Id == monster.InitialState);
			var animation = monster.Animations.FirstOrDefault(a => a.Id == state?.Animation);
			AddChild(new SpriteDefinitionView( animation?.Frames.First().SpriteDefinition ?? new NullSpriteDefinition()));
			AddChild(new Label { Text = monster.Name});
		}
	}
}