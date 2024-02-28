namespace DemonCastle.Files.Actions;

public class ItemActionData {
	public PlayerActionData? Player { get; set; }

	public void Clear() {
		Player = null;
	}
}

public class PlayerActionData {
	public int? RecoverHp { get; set; }
	public int? RecoverMp { get; set; }

	public void Clear() {
		RecoverHp = null;
		RecoverMp = null;
	}
}