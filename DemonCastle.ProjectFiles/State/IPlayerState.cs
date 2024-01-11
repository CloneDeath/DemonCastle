using System.ComponentModel;
using Godot;

namespace DemonCastle.ProjectFiles.State;

public interface IPlayerState : INotifyPropertyChanged {
	public int HP { get; }
	public int MaxHP { get; }
	public int MP { get; }
	public int MaxMP { get; }
	public int Lives { get; }
	public int? MaxLives { get; }
	public int Score { get; }
	Vector2 Position { get; }
}