using DemonCastle.Files.BaseEntity;

namespace DemonCastle.Files;

public class MonsterFile : BaseEntityFile , IGameFile {
	public int FileVersion => 1;
	public int Health = 1;
	public int Experience = 1;
	public int Attack = 1;
	public int PhysicalDefense;
	public int MagicalDefense;

	public float MoveSpeed = 3;
	public float JumpHeight = 3;
	public float Gravity = 100;

	public bool DespawnOnDeath = true;
}