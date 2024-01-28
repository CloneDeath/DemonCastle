using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using DemonCastle.Editor.Editors.Components.Properties;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools.Edit;

public partial class MonsterDataEdit : PropertyCollection {
	private readonly MonsterDataInfoProxy _proxy = new();
	public MonsterDataInfo? MonsterData {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public MonsterDataEdit(ProjectInfo project) {
		AddMonsterReference("Monster", _proxy, p => p.MonsterId, new EnumerableInfoWrapper<MonsterInfo>(project.Monsters));
		AddVector2("Position", _proxy, p => p.Position);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (MonsterData != null) Enable();
		else Disable();
	}
}