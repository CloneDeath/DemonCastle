using System;
using DemonCastle.Game.DebugNodes;
using DemonCastle.Game.GameStates;
using DemonCastle.Game.Scenes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Game;

public partial class GameRunner : Control {
	private readonly ProjectInfo _project;
	private readonly ProjectResources _resources;
	public SceneStack SceneStack { get; }
	public GameLevel Level { get; }
	public GamePlayer GamePlayer { get; }
	public SubViewport LevelViewport { get; }

	public GameArea? CurrentArea { get; set; }

	public GameRunner(ProjectResources resources, ProjectInfo project, DebugState? debug = null) {
		_project = project;
		_resources = resources;
		debug ??= new DebugState();
		Name = nameof(GameRunner);
		TextureFilter = TextureFilterEnum.Nearest;

		var gameState = new GameState(project, this);

		AddChild(SceneStack = new SceneStack(gameState));

		AddChild(LevelViewport = new SubViewport {
			Name = "GameView"
		});
		var logger = new GameLogger(debug);
		LevelViewport.AddChild(Level = new GameLevel(resources, gameState, logger, debug));
		LevelViewport.AddChild(GamePlayer = new GamePlayer(gameState, logger, debug) {
			Position = Level.StartingLocation
		});
		LevelViewport.AddChild(new GameCamera(GamePlayer, Level));

		SceneStack.Set(project.StartScene);
		AddChild(new FramePerSecondLabel(debug));
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

		CurrentArea?.OnPlayerExit();
		CurrentArea = area;
		CurrentArea?.OnPlayerEnter();
	}

	public void SetCharacter(CharacterInfo character) => GamePlayer.LoadCharacter(character);

	public void SetLevel(LevelInfo level) {
		LevelViewport.Size = level.AreaScale.ToPixelSize();
		GamePlayer.LoadLevel(level);
		Level.LoadLevel(level);
	}

	public void SpawnItem(Guid itemId, Vector2 position) {
		if (CurrentArea == null) return;
		var item = _resources.GetItem(itemId);
		if (item == null) return;
		CurrentArea.SpawnItem(item, position);
	}

	public void Restart() {
		GamePlayer.Reset();
		Level.Reset();
		SceneStack.Set(_project.StartScene);
	}

	public void Quit() => GetTree().Quit();
}