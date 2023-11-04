using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GameRunner : Node2D {
	protected GameLevel Level { get; }
	protected GamePlayer Player { get; }
	public GameRunner(LevelInfo level, CharacterInfo player) {
		AddChild(Level = new GameLevel(level));
		AddChild(Player = new GamePlayer(level, player) {
			Position = Level.StartingLocation
		});
	}
}