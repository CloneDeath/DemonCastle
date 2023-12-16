using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools.Edit;

public partial class MonsterDataEdit : PropertyCollection {
	private readonly MonsterDataInfoProxy _proxy = new();
	public MonsterDataInfo? MonsterData {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public MonsterDataEdit(ProjectInfo project) {
		AddMonsterReference("Monster", _proxy, p => p.MonsterId, project.Monsters);
		AddVector2("Position", _proxy, p => p.Position);
	}
}