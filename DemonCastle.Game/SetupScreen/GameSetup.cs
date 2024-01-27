using System;
using DemonCastle.Game.DebugNodes;
using Godot;

namespace DemonCastle.Game.SetupScreen;

public partial class GameSetup : VBoxContainer {
	public event Action<DebugState>? GameStart;

	protected CheckBox Debug_ShowPositions { get; }
	protected CheckBox Debug_ShowCollisions { get; }
	protected CheckBox Debug_ShowHitBoxes { get; }
	protected CheckBox Debug_ShowHurtBoxes { get; }
	protected CheckBox Debug_ShowFramesPerSecond { get; }
	protected CheckBox Debug_LogStateChanges { get; }

	protected Button LaunchButton { get; }


	public GameSetup() {
		Name = nameof(GameSetup);

		AddChild(Debug_ShowPositions = new CheckBox {
			Text = "Show Positions",
			ButtonPressed = false
		});
		AddChild(Debug_ShowCollisions = new CheckBox {
			Text = "Show Collisions",
			ButtonPressed = false
		});
		AddChild(Debug_ShowHitBoxes = new CheckBox {
			Text = "Show HitBoxes",
			ButtonPressed = false
		});
		AddChild(Debug_ShowHurtBoxes = new CheckBox {
			Text = "Show HurtBoxes",
			ButtonPressed = false
		});
		AddChild(Debug_ShowFramesPerSecond = new CheckBox {
			Text = "Show Frames Per Second",
			ButtonPressed = false
		});
		AddChild(Debug_LogStateChanges = new CheckBox {
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
			ShowPositions = Debug_ShowPositions.ButtonPressed,
			ShowCollisions = Debug_ShowCollisions.ButtonPressed,
			ShowHitBoxes = Debug_ShowHitBoxes.ButtonPressed,
			ShowHurtBoxes = Debug_ShowHurtBoxes.ButtonPressed,
			ShowFramesPerSecond = Debug_ShowFramesPerSecond.ButtonPressed,
			LogStateChanges = Debug_LogStateChanges.ButtonPressed
		};
		GameStart?.Invoke(debug);
	}
}