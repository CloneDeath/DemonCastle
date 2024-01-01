using DemonCastle.Game.DebugNodes;
using DemonCastle.Game.Scenes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public partial class GameRunner : Control, IGameState {
	private SceneStack SceneStack { get; }
	public GameLevel Level { get; }
	public GamePlayer Player { get; }

	protected GameArea? CurrentArea { get; set; }

	public GameRunner(ProjectInfo project, DebugState? debug = null) {
		debug ??= new DebugState();
		Name = nameof(GameRunner);
		TextureFilter = TextureFilterEnum.Nearest;

		AddChild(SceneStack = new SceneStack(this));

		var subViewport = new SubViewport();
		AddChild(subViewport);

		subViewport.AddChild(Level = new GameLevel(project, debug));
		subViewport.AddChild(Player = new GamePlayer(debug, new GameLogger()) {
			Position = Level.StartingLocation
		});
		subViewport.AddChild(new GameCamera(Player, Level));

		SetScene(project.StartScene);
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

		var area = Level.GetGameAreaAtPoint((Vector2I)Player.Position);
		if (CurrentArea == area) return;

		CurrentArea = area;
		CurrentArea?.OnPlayerEnter();
	}

	public void SetCharacter(CharacterInfo character) {
		Player.LoadCharacter(character);
	}

	public void SetLevel(LevelInfo level) {
		Level.LoadLevel(level);
		Player.Position = Level.StartingLocation;
	}

	public void SetScene(SceneInfo scene) => SceneStack.Set(scene);
	public void PushScene(SceneInfo scene) => SceneStack.Push(scene);
	public void PopScene(int number) => SceneStack.Pop(number);

	public IInputState Input => new InputState();
}