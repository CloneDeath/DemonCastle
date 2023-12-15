using System.Linq;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Level.Area.Details;
using DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools.List;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools;

public partial class MonsterToolsPanel : VBoxContainer {
	private readonly AreaProxy _proxy = new();

	public AreaInfo? Area {
		get => _proxy.Proxy;
		set {
			_proxy.Proxy = value;
			MonsterList.Load(value);
		}
	}

	private MonsterDataList MonsterList { get; }

	public MonsterToolsPanel(ProjectInfo project) {
		Name = nameof(MonsterToolsPanel);

		AddChild(MonsterList = new MonsterDataList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});

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