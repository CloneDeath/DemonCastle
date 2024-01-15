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
		set {
			Data.Health = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int Experience {
		get => Data.Experience;
		set {
			Data.Experience = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int Attack {
		get => Data.Attack;
		set {
			Data.Attack = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int PhysicalDefense {
		get => Data.PhysicalDefense;
		set {
			Data.PhysicalDefense = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int MagicalDefense {
		get => Data.MagicalDefense;
		set {
			Data.MagicalDefense = value;
			Save();
			OnPropertyChanged();
		}
	}

	public float MoveSpeed {
		get => Data.MoveSpeed;
		set {
			Data.MoveSpeed = value;
			Save();
			OnPropertyChanged();
		}
	}

	public float JumpHeight {
		get => Data.JumpHeight;
		set {
			Data.JumpHeight = value;
			Save();
			OnPropertyChanged();
		}
	}

	public float Gravity {
		get => Data.Gravity;
		set {
			Data.Gravity = value;
			Save();
			OnPropertyChanged();
		}
	}

	public bool DespawnOnDeath {
		get => Data.DespawnOnDeath;
		set => SaveField(ref Data.DespawnOnDeath, value);
	}
}