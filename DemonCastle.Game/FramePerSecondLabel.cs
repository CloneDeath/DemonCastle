using Godot;

namespace DemonCastle.Game;

public partial class FramePerSecondLabel : Label{
	public override void _Process(double delta) {
		base._Process(delta);
		Text = $"{Engine.GetFramesPerSecond()}";
	}
}