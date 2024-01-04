using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class MonsterInfo : BaseEntityInfo<MonsterFile> {
	public MonsterInfo(FileNavigator<MonsterFile> file) : base(file) { }

	public int Health {
		get => Resource.Health;
		set {
			Resource.Health = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int Experience {
		get => Resource.Experience;
		set {
			Resource.Experience = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int Attack {
		get => Resource.Attack;
		set {
			Resource.Attack = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int PhysicalDefense {
		get => Resource.PhysicalDefense;
		set {
			Resource.PhysicalDefense = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int MagicalDefense {
		get => Resource.MagicalDefense;
		set {
			Resource.MagicalDefense = value;
			Save();
			OnPropertyChanged();
		}
	}

	public float MoveSpeed {
		get => Resource.MoveSpeed;
		set {
			Resource.MoveSpeed = value;
			Save();
			OnPropertyChanged();
		}
	}

	public float JumpHeight {
		get => Resource.JumpHeight;
		set {
			Resource.JumpHeight = value;
			Save();
			OnPropertyChanged();
		}
	}

	public float Gravity {
		get => Resource.Gravity;
		set {
			Resource.Gravity = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2I Size {
		get => new(Resource.Size.Width, Resource.Size.Height);
		set {
			Resource.Size.Width = value.X;
			Resource.Size.Height = value.Y;
			Save();
			OnPropertyChanged();
		}
	}
}