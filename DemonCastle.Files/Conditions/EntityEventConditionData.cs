namespace DemonCastle.Files.Conditions;

public class EntityEventConditionData {
	public PlayerPositionTransition? OnPlayer;

	public void Clear() {
		OnPlayer = null;
	}
}

public enum PlayerPositionTransition {
	Enter,
	Exit
}