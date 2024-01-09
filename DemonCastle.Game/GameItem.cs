using DemonCastle.Game.BaseEntities;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.Game;

public partial class GameItem : GameBaseEntity {
	public GameItem(IGameState game, LevelInfo level, ItemInfo item, IGameLogger logger, DebugState debug) : base(game,
		level, item, logger, debug) {
		Name = $"{nameof(GameItem)} {item.Name} ({Id})";
	}
	public override bool WasKilled => false;

	public override float MoveSpeed => 0;
	protected override float Gravity => 0;
}