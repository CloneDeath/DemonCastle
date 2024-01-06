using DemonCastle.Files.Actions.ActionEnums;
using DemonCastle.Files.Actions.DataValues;

namespace DemonCastle.Files.Actions;

public class EntityActionData {
	public FaceAction? Face;
	public MoveAction? Move;
	public SelfAction? Self;

	public ReferenceData? SpawnMonster { get; set; }
	public ReferenceData? SpawnItem { get; set; }
}

public class ActionSpawnData {
	public ReferenceData Instance { get; set; } = new();
	public Vector2IDataValue Offset { get; set; } = new();
	public RelativeTo RelativeTo { get; set; } = RelativeTo.Self;
}

public enum RelativeTo {
	Self,
	Area
}