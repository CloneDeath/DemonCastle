using System.ComponentModel;
using Godot;

namespace DemonCastle.ProjectFiles.State;

public interface IPlayerState : INotifyPropertyChanged {
	public int Hp { get; }
	public int MaxHp { get; }
	public void RecoverHp(int amount);

	public int Mp { get; }
	public int MaxMp { get; }
	public void RecoverMp(int amount);

	public int Lives { get; }
	public int? MaxLives { get; }
	public int Score { get; }
	Vector2 Position { get; }
}