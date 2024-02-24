using DemonCastle.Game.BaseEntities;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.Game;

public partial class GameItem : GameBaseEntity {
	private readonly ItemInfo _item;

	public GameItem(IGameState game, LevelInfo level, ItemInfo item, IGameLogger logger, DebugState debug) : base(game,
		level, item, logger, debug) {
		_item = item;
		Name = $"{nameof(GameItem)} {item.Name} ({Id})";
	}

	protected override bool IsImmobile => false;
	public override bool WasKilled => false;

	public override float MoveSpeed => _item.MoveSpeed * Level.TileSize.X;
	public override float JumpHeight => 0 * Level.TileSize.Y;
	protected override float Gravity => _item.Gravity * Level.TileSize.Y;
	protected override bool ApplyDamage(int amount) => false;
}