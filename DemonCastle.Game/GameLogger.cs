using System;
using DemonCastle.Game.DebugNodes;
using DemonCastle.Game.PlayerStates;
using Godot;

namespace DemonCastle.Game;

public interface IGameLogger {
	void StateChanged(IState state);
}

public class GameLogger : IGameLogger {
	private readonly DebugState _debug;

	public GameLogger(DebugState debug) {
		_debug = debug;
	}

	public void StateChanged(IState state) {
		if (!_debug.LogStateChanges) return;
		GD.Print($"[{DateTime.Now}] State - {state.GetType().Name}");
	}
}