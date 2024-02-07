using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using Godot;
using Container = Godot.Container;

namespace DemonCastle.Editor.Editors.Level.Area.View.Monsters;

public partial class MonsterDataView : Container {
	private readonly ProjectInfo _project;
	private readonly MonsterDataInfo _monsterData;
	private SpriteDefinitionView Sprite { get; }

	public MonsterDataView(ProjectInfo project, MonsterDataInfo monsterData) {
		_project = project;
		_monsterData = monsterData;
		Name = nameof(MonsterDataView);

		AddChild(Sprite = new SpriteDefinitionView());

		Reload();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_monsterData.PropertyChanged += MonsterData_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_monsterData.PropertyChanged -= MonsterData_OnPropertyChanged;
	}

	private void MonsterData_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Reload();
	}

	private void Reload() {
		Position = _monsterData.MonsterPosition.ToPixelPositionInArea();
		var monster = _project.Monsters.FirstOrDefault(m => m.Id == _monsterData.MonsterId);
		Sprite.Load(monster?.PreviewSpriteDefinition);
		Sprite.Position = -monster?.PreviewOrigin ?? Vector2.Zero;
	}
}