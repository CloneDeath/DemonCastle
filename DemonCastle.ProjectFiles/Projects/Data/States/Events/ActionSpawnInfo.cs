using DemonCastle.Files.Actions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Events;

public class ActionSpawnMonsterInfo : ActionSpawnInfo {
	protected override string Type => "Monster";
	public ActionSpawnMonsterInfo(IFileNavigator file, EntityActionData data) : base(file, data, data.SpawnMonster) { }
	protected override ActionSpawnData? SpawnData {
		get => Data.SpawnMonster;
		set => Data.SpawnMonster = value;
	}
}

public class ActionSpawnItemInfo : ActionSpawnInfo {
	protected override string Type => "Item";
	public ActionSpawnItemInfo(IFileNavigator file, EntityActionData data) : base(file, data, data.SpawnItem) {	}
	protected override ActionSpawnData? SpawnData {
		get => Data.SpawnItem;
		set => Data.SpawnItem = value;
	}
}

public abstract class ActionSpawnInfo : BaseInfo<EntityActionData>, IListableInfo {
	private readonly ActionSpawnData _data;

	protected abstract string Type { get; }

	protected ActionSpawnInfo(IFileNavigator file, EntityActionData actionData, ActionSpawnData? spawnData) : base(file, actionData) {
		_data = spawnData ?? new ActionSpawnData();
		Instance = new ReferenceInfo(file, _data.Instance);
		Offset = new Vector2IValueInfo(file, _data.Offset);
	}

	public string ListLabel => $"Spawn {Type} {Instance.ListLabel} at {Offset.ListLabel}";
	public bool IsSet {
		get => SpawnData != null;
		set {
			Data.Clear();
			SpawnData = value ? _data : null;
			Save();
		}
	}

	protected abstract ActionSpawnData? SpawnData { get; set; }

	public ReferenceInfo Instance { get; }
	public Vector2IValueInfo Offset { get; }

	public RelativeTo RelativeTo {
		get => _data.RelativeTo;
		set => SaveField(ref _data.RelativeTo, value);
	}
}