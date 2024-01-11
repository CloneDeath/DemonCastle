using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public class PlayerVariables : IPlayerState {
	private readonly GamePlayer _player;

	public PlayerVariables(GamePlayer player) {
		_player = player;
	}

	private int _hp = 10;
	public int HP {
		get => _hp;
		set => SetField(ref _hp, value);
	}

	private int _maxHP = 10;
	public int MaxHP {
		get => _maxHP;
		set => SetField(ref _maxHP, value);
	}

	private int _mp = 8;
	public int MP {
		get => _mp;
		set => SetField(ref _mp, value);
	}

	private int _maxMP = 8;
	public int MaxMP {

		get => _maxMP;
		set => SetField(ref _maxMP, value);
	}

	private int _lives = 1;
	public int Lives {
		get => _lives;
		set => SetField(ref _lives, value);
	}

	private int? _maxLives;
	public int? MaxLives {
		get => _maxLives;
		set => SetField(ref _maxLives, value);
	}

	private int _score;
	public int Score {
		get => _score;
		set => SetField(ref _score, value);
	}

	public Vector2 Position => _player.PositionInArea;

	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	#endregion
}