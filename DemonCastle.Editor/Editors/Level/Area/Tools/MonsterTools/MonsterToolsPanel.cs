using DemonCastle.Editor.Editors.Level.Area.Details;
using DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools.Edit;
using DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools.List;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using DemonCastle.ProjectFiles.Projects.Resources;
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

	public MonsterToolsPanel(ProjectResources resources) {
		Name = nameof(MonsterToolsPanel);

		AddChild(MonsterList = new MonsterDataList(resources) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		MonsterList.MonsterSelected += MonsterList_OnMonsterSelected;

		AddChild(MonsterEdit = new MonsterDataEdit(resources));
	}

	private void MonsterList_OnMonsterSelected(MonsterDataInfo? data) {
		MonsterEdit.MonsterData = data;
	}
}