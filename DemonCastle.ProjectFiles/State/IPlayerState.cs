using Godot;

namespace DemonCastle.ProjectFiles.State;

public interface IPlayerState {
	public int HP { get; }
	public int MP { get; }
	public int Lives { get; }
	public int Score { get; }
	Vector2 Position { get; }
}