using System;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game.SetupScreen;

public partial class GameSetup : Container {
	public event Action<LevelInfo, CharacterInfo, DebugState>? GameStart;

	protected Label CharactersLabel { get; }
	protected InfoItemList<CharacterInfo> CharacterInfoList { get; }
	protected Label LevelsLabel { get; }
	protected InfoItemList<LevelInfo> LevelInfoList { get; }
	protected Button LaunchButton { get; }

	protected CheckBox Debug_ShowPositions { get; }
	protected CheckBox Debug_ShowCollisions { get; }
	protected CheckBox Debug_ShowHitBoxes { get; }
	protected CheckBox Debug_ShowHurtBoxes { get; }
	protected CheckBox Debug_LogStateChanges { get; }

	public GameSetup(ProjectInfo project) {
		Name = nameof(GameSetup);

		AddChild(CharactersLabel = new Label {
			Text = "Characters:",
			Position = new Vector2(10, 10)
		});
		AddChild(CharacterInfoList = new InfoItemList<CharacterInfo>(project.Characters) {
			Position = CharactersLabel.Position + new Vector2(0, 30)
		});
		CharacterInfoList.Select(0);

		AddChild(LevelsLabel = new Label {
			Text = "Levels:",
			Position = CharactersLabel.Position + new Vector2(310, 0)
		});
		AddChild(LevelInfoList = new InfoItemList<LevelInfo>(project.Levels) {
			Position = LevelsLabel.Position + new Vector2(0, 30)
		});
		LevelInfoList.Select(0);

		AddChild(LaunchButton = new Button {
			Text = "Start Chapter",
			Position = CharacterInfoList.Position + new Vector2(0, 310)
		});
		LaunchButton.Pressed += OnLaunchButtonClicked;

		var debugOptions = new VBoxContainer {
			Position = LevelsLabel.Position + new Vector2(310, 0)
		};
		AddChild(debugOptions);
		debugOptions.AddChild(Debug_ShowPositions = new CheckBox {
			Text = "Show Positions",
			ButtonPressed = false
		});
		debugOptions.AddChild(Debug_ShowCollisions = new CheckBox {
			Text = "Show Collisions",
			ButtonPressed = false
		});
		debugOptions.AddChild(Debug_ShowHitBoxes = new CheckBox {
			Text = "Show HitBoxes",
			ButtonPressed = false
		});
		debugOptions.AddChild(Debug_ShowHurtBoxes = new CheckBox {
			Text = "Show HurtBoxes",
			ButtonPressed = false
		});
		debugOptions.AddChild(Debug_LogStateChanges = new CheckBox {
			Text = "Log State Changes",
			ButtonPressed = false
		});
	}

	public override void _Process(double delta) {
		base._Process(delta);

		LaunchButton.Disabled = LevelInfoList.NoItemSelected || CharacterInfoList.NoItemSelected;
	}

	protected void OnLaunchButtonClicked() {
		var debug = new DebugState {
			ShowPositions = Debug_ShowPositions.ButtonPressed,
			ShowCollisions = Debug_ShowCollisions.ButtonPressed,
			ShowHitBoxes = Debug_ShowHitBoxes.ButtonPressed,
			ShowHurtBoxes = Debug_ShowHurtBoxes.ButtonPressed,
			LogStateChanges = Debug_LogStateChanges.ButtonPressed
		};
		GameStart?.Invoke(LevelInfoList.SelectedItem, CharacterInfoList.SelectedItem, debug);
	}
}