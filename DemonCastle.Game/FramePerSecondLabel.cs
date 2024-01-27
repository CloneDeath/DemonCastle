using DemonCastle.Game.DebugNodes;
using Godot;

namespace DemonCastle.Game;

public partial class FramePerSecondLabel : Label{
	public FramePerSecondLabel(DebugState debug) {
		Visible = debug.ShowFramesPerSecond;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Text = $"{Engine.GetFramesPerSecond()}";
	}
}