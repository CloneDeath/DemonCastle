using Godot;

namespace DemonCastle.Game;

public partial class GameCamera : Camera2D {
	private readonly GamePlayer _player;
	private readonly GameLevel _level;

	public GameCamera(GamePlayer player, GameLevel level) {
		_player = player;
		_level = level;

		Name = nameof(GameCamera);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		SetLimits();

		Position = _player.Position;
	}

	private void SetLimits() {
		var area = _level.GetAreaAtPoint((Vector2I)_player.Position);
		if (area == null) return;

		var start = area.PositionOfArea.ToPixelPositionInLevel();
		LimitLeft = start.X;
		LimitTop = start.Y;

		var end = start + area.SizeOfArea.ToPixelSize();
		LimitRight = end.X;
		LimitBottom = end.Y;
	}
}