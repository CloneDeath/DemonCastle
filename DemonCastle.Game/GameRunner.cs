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

		var subViewportContainer = new SubViewportContainer {
			Stretch = false,
			Scale = Vector2.One * 3
		};
		AddChild(subViewportContainer);
		subViewportContainer.SetAnchorsPreset(LayoutPreset.FullRect);

		var subViewport = new SubViewport {
			Size = level.AreaScale.ToPixelSize()
		};
		subViewportContainer.AddChild(subViewport);

		subViewport.AddChild(Level = new GameLevel(level));
		subViewport.AddChild(Player = new GamePlayer(level, player, new GameLogger()) {
			Position = Level.StartingLocation
		});
		subViewport.AddChild(new GameCamera(Player, Level));
	}
}