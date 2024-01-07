using System;
using DemonCastle.Game.PlayerStates;
using Godot;

namespace DemonCastle.Game;

public interface IGameLogger {
	void StateChanged(IState state);
}

public class GameLogger : IGameLogger {
	public void StateChanged(IState state) {
		GD.Print($"[{DateTime.Now}] State - {state.GetType().Name}");
	}
}