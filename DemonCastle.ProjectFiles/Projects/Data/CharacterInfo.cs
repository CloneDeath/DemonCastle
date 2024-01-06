using System;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class CharacterInfo : FileInfo<CharacterFile>, IListableInfo {
	public CharacterInfo(FileNavigator<CharacterFile> file) : base(file) {
		Animations = new AnimationInfoCollection(file, Resource.Animations);
	}

	public string ListLabel => Name;

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
		}
	}

	public AnimationInfoCollection Animations { get; }

	public float WalkSpeed {
		get => Resource.WalkSpeed;
		set {
			Resource.WalkSpeed = value;
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

	public float Width {
		get => Resource.Width;
		set {
			Resource.Width = value;
			Save();
			OnPropertyChanged();
		}
	}

	public float Height {
		get => Resource.Height;
		set {
			Resource.Height = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string DefaultWeapon {
		get => Resource.DefaultWeapon;
		set {
			Resource.DefaultWeapon = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(DefaultWeaponInfo));
		}
	}

	public WeaponInfo? DefaultWeaponInfo => File.FileExists(DefaultWeapon) ? File.GetWeapon(DefaultWeapon) : null;

	public Guid IdleAnimation {
		get => Resource.IdleAnimation;
		set {
			Resource.IdleAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid WalkAnimation {
		get => Resource.WalkAnimation;
		set {
			Resource.WalkAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid JumpAnimation {
		get => Resource.JumpAnimation;
		set {
			Resource.JumpAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid CrouchAnimation {
		get => Resource.CrouchAnimation;
		set {
			Resource.CrouchAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid StairsUpAnimation {
		get => Resource.StairsUpAnimation;
		set {
			Resource.StairsUpAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid StairsDownAnimation {
		get => Resource.StairsDownAnimation;
		set {
			Resource.StairsDownAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid StandAttackAnimation {
		get => Resource.StandAttackAnimation;
		set {
			Resource.StandAttackAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid JumpAttackAnimation {
		get => Resource.JumpAttackAnimation;
		set {
			Resource.JumpAttackAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid StairsUpAttackAnimation {
		get => Resource.StairsUpAttackAnimation;
		set {
			Resource.StairsUpAttackAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid StairsDownAttackAnimation {
		get => Resource.StairsDownAttackAnimation;
		set {
			Resource.StairsDownAttackAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2 Size => new(Resource.Width, Resource.Height);
}