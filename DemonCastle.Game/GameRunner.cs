using System;
using System.Linq;
using DemonCastle.Game.DebugNodes;
using DemonCastle.Game.GameStates;
using DemonCastle.Game.Scenes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game;

public partial class GameRunner : Control {
	private readonly ProjectInfo _project;
	public SceneStack SceneStack { get; }
	public GameLevel Level { get; }
	public GamePlayer GamePlayer { get; }
	public SubViewport LevelViewport { get; }

	public GameArea? CurrentArea { get; set; }

	public GameRunner(ProjectInfo project, DebugState? debug = null) {
		_project = project;
		debug ??= new DebugState();
		Name = nameof(GameRunner);
		TextureFilter = TextureFilterEnum.Nearest;

		var gameState = new GameState(this);

		AddChild(SceneStack = new SceneStack(gameState));

		AddChild(LevelViewport = new SubViewport());
		LevelViewport.AddChild(Level = new GameLevel(project, gameState, debug));
		LevelViewport.AddChild(GamePlayer = new GamePlayer(gameState, debug, new GameLogger(debug)) {
			Position = Level.StartingLocation
		});
		LevelViewport.AddChild(new GameCamera(GamePlayer, Level));

		SceneStack.Set(project.StartScene);
	}

	public override void _EnterTree() {
		base._EnterTree();
		GetTree().DebugCollisionsHint = true;
	}

	public override void _ExitTree() {
		base._ExitTree();
		GetTree().DebugCollisionsHint = false;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Size = SceneStack.SceneSize;
		PivotOffset = SceneStack.SceneSize / 2;
		Position = GetParentAreaSize() / 2 - SceneStack.SceneSize / 2;
		var screenScale = (GetParentAreaSize() / SceneStack.SceneSize).Floor();
		var screenFactor = Math.Max(1, Math.Min(screenScale.X, screenScale.Y));
		Scale = Vector2.One * screenFactor;

		var area = Level.GetGameAreaAtPoint((Vector2I)GamePlayer.Position);
		if (CurrentArea == area) return;

		CurrentArea = area;
		CurrentArea?.OnPlayerEnter();
	}

	public void SetCharacter(CharacterInfo character) => GamePlayer.LoadCharacter(character);

	public void SetLevel(LevelInfo level) {
		Level.LoadLevel(level);
		GamePlayer.LoadLevel(level);
		LevelViewport.Size = level.AreaScale.ToPixelSize();
	}

	public void SpawnItem(Guid itemId, Vector2 position) {
		if (CurrentArea == null) return;
		var item = _project.Items.First(i => i.Id == itemId);
		CurrentArea.SpawnItem(item, position);
	}
}