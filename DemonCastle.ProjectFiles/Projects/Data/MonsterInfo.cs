using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class MonsterInfo : BaseEntityInfo<MonsterFile>, IFileInfo {
	public MonsterInfo(FileNavigator<MonsterFile> file) : base(file, file.Resource) { }

	public string FileName => File.FileName;
	public string Directory => File.Directory;
	void IFileInfo.Save() => base.Save();

	public int Health {
		get => Data.Health;
		set => SaveField(ref Data.Health, value);
	}

	public int Experience {
		get => Data.Experience;
		set => SaveField(ref Data.Experience, value);
	}

	public int Attack {
		get => Data.Attack;
		set => SaveField(ref Data.Attack, value);
	}

	public int PhysicalDefense {
		get => Data.PhysicalDefense;
		set => SaveField(ref Data.PhysicalDefense, value);
	}

	public int MagicalDefense {
		get => Data.MagicalDefense;
		set => SaveField(ref Data.MagicalDefense, value);
	}

	public float MoveSpeed {
		get => Data.MoveSpeed;
		set => SaveField(ref Data.MoveSpeed, value);
	}

	public float JumpHeight {
		get => Data.JumpHeight;
		set => SaveField(ref Data.JumpHeight, value);
	}

	public float Gravity {
		get => Data.Gravity;
		set => SaveField(ref Data.Gravity, value);
	}

	public bool DespawnOnDeath {
		get => Data.DespawnOnDeath;
		set => SaveField(ref Data.DespawnOnDeath, value);
	}
}