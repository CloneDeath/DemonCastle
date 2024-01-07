using DemonCastle.Files.BaseEntity;

namespace DemonCastle.Files;

public class MonsterFile : BaseEntityFile {
	public int Health { get; set; } = 1;
	public int Experience { get; set; } = 1;
	public int Attack { get; set; } = 1;
	public int PhysicalDefense { get; set; }
	public int MagicalDefense { get; set; }

	public float MoveSpeed { get; set; } = 3;
	public float JumpHeight { get; set; } = 3;
	public float Gravity { get; set; } = 100;

	public bool DespawnOnDeath = true;
}