using DemonCastle.Game.BaseEntities;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.Game;

public partial class GameMonster : GameBaseEntity {
	private readonly MonsterInfo _monster;
	private readonly MonsterDataInfo _monsterData;

	public int Hp { get; set; }
	public int MaxHp => _monster.Health;

	public GameMonster(IGameState game, LevelInfo level, MonsterInfo monster, MonsterDataInfo monsterData, IGameLogger logger, DebugState debug)
		: base(game, level, monster, logger, debug) {
		_monster = monster;
		_monsterData = monsterData;
		Name = $"{nameof(GameMonster)} {monster.Name} ({Id})";
	}

	public override float MoveSpeed => _monster.MoveSpeed * Level.TileSize.X;
	protected override float Gravity => _monster.Gravity;

	protected override bool ApplyDamage(int amount) {
		Hp -= amount;
		if (_monster.DespawnOnDeath && Hp <= 0) {
			Despawn();
		}
		return true;
	}

	public override void Reset() {
		Position = _monsterData.MonsterPosition.ToPixelPositionInArea();
		Hp = MaxHp;

		base.Reset();
	}

	public override bool WasKilled => Hp < 0;
}