using System;
using DemonCastle.Game.DebugNodes;
using Godot;

namespace DemonCastle.Game.SetupScreen;

public partial class GameSetup : VBoxContainer {
	public event Action<DebugState>? GameStart;

	protected CheckBox DebugShowPositions { get; }
	protected CheckBox DebugShowCollisions { get; }
	protected CheckBox DebugShowHitBoxes { get; }
	protected CheckBox DebugShowHurtBoxes { get; }
	protected CheckBox DebugShowFramesPerSecond { get; }
	protected CheckBox DebugLogStateChanges { get; }

	protected Button LaunchButton { get; }


	public GameSetup() {
		Name = nameof(GameSetup);

		AddChild(DebugShowPositions = new CheckBox {
			Text = "Show Positions",
			ButtonPressed = false
		});
		AddChild(DebugShowCollisions = new CheckBox {
			Text = "Show Collisions",
			ButtonPressed = false
		});
		AddChild(DebugShowHitBoxes = new CheckBox {
			Text = "Show HitBoxes",
			ButtonPressed = false
		});
		AddChild(DebugShowHurtBoxes = new CheckBox {
			Text = "Show HurtBoxes",
			ButtonPressed = false
		});
		AddChild(DebugShowFramesPerSecond = new CheckBox {
			Text = "Show Frames Per Second",
			ButtonPressed = false
		});
		AddChild(DebugLogStateChanges = new CheckBox {
			Text = "Log State Changes",
			ButtonPressed = false
		});

		AddChild(LaunchButton = new Button {
			Text = "Start Game"
		});
		LaunchButton.Pressed += OnLaunchButtonClicked;
	}

	protected void OnLaunchButtonClicked() {
		var debug = new DebugState {
			ShowPositions = DebugShowPositions.ButtonPressed,
			ShowCollisions = DebugShowCollisions.ButtonPressed,
			ShowHitBoxes = DebugShowHitBoxes.ButtonPressed,
			ShowHurtBoxes = DebugShowHurtBoxes.ButtonPressed,
			ShowFramesPerSecond = DebugShowFramesPerSecond.ButtonPressed,
			LogStateChanges = DebugLogStateChanges.ButtonPressed
		};
		GameStart?.Invoke(debug);
	}
}