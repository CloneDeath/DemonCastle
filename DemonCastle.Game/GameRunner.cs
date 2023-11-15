using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GameRunner : Control {
	protected GameLevel Level { get; }
	protected GamePlayer Player { get; }
	public GameRunner(LevelInfo level, CharacterInfo player) {
		Name = nameof(GameRunner);
		TextureFilter = TextureFilterEnum.Nearest;

		AddChild(Level = new GameLevel(level));
		AddChild(Player = new GamePlayer(level, player, new GameLogger()) {
			Position = Level.StartingLocation
		});
		AddChild(new GameCamera(Player, Level) {
			Zoom = Vector2.One * 3
		});
	}
}