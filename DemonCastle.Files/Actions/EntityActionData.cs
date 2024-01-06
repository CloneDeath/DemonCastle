using DemonCastle.Files.Actions.ActionEnums;
using DemonCastle.Files.Actions.Values;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Actions;

public class EntityActionData {
	public FaceAction? Face;
	public MoveAction? Move;
	public SelfAction? Self;

	public ActionSpawnData? SpawnMonster;
	public ActionSpawnData? SpawnItem;

	public void Clear() {
		Face = null;
		Move = null;
		Self = null;
		SpawnMonster = null;
		SpawnItem = null;
	}
}

public class ActionSpawnData {
	public ReferenceData Instance { get; set; } = new();
	public Vector2IValueData Offset { get; set; } = new();
	public RelativeTo RelativeTo = RelativeTo.Self;
}

[JsonConverter(typeof(StringEnumConverter))]
public enum RelativeTo {
	Self,
	Area
}