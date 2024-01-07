using DemonCastle.Game.BaseEntities;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.Game;

public partial class GameItem : GameBaseEntity {
	public GameItem(IGameState game, ItemInfo item, DebugState debug) : base(game, item, debug) { }
	public override bool WasKilled => false;
}