using DemonCastle.Editor.Editors.Level.Area.Details;
using DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools.Edit;
using DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools.List;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
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
	private MonsterDataEdit MonsterEdit { get; }

	public MonsterToolsPanel(ProjectInfo project) {
		Name = nameof(MonsterToolsPanel);

		AddChild(MonsterList = new MonsterDataList(project) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		MonsterList.MonsterSelected += MonsterList_OnMonsterSelected;

		AddChild(MonsterEdit = new MonsterDataEdit(project));
	}

	private void MonsterList_OnMonsterSelected(MonsterDataInfo? data) {
		MonsterEdit.MonsterData = data;
	}
}