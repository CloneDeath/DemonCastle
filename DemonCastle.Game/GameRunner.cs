using System;
using DemonCastle.Game.DebugNodes;
using DemonCastle.Game.Scenes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public partial class GameRunner : Control, IGameState {
	private SceneStack SceneStack { get; }

	public GameLevel? Level { get; }
	public GamePlayer? Player { get; }
	protected GameArea? CurrentArea { get; set; }

	public GameRunner(ProjectInfo project, DebugState? debug = null) {
		debug ??= new DebugState();
		Name = nameof(GameRunner);
		TextureFilter = TextureFilterEnum.Nearest;

		AddChild(SceneStack = new SceneStack(this));
		SetScene(project.StartScene);


		/*
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

		subViewport.AddChild(Level = new GameLevel(project, level, debug));
		subViewport.AddChild(Player = new GamePlayer(level, player, debug, new GameLogger()) {
			Position = Level.StartingLocation
		});
		subViewport.AddChild(new GameCamera(Player, Level));

		*/
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
		if (Level == null || Player == null) return;

		var area = Level.GetGameAreaAtPoint((Vector2I)Player.Position);
		if (CurrentArea == area) return;

		CurrentArea = area;
		CurrentArea?.OnPlayerEnter();
	}

	public void SetCharacter(CharacterInfo character) {
		throw new NotImplementedException();
	}

	public void SetLevel(LevelInfo level) {
		throw new NotImplementedException();
	}

	public void SetScene(SceneInfo scene) => SceneStack.Set(scene);
	public void PushScene(SceneInfo scene) => SceneStack.Push(scene);
	public void PopScene(int number) => SceneStack.Pop(number);

	public IInputState Input => new InputState();
}