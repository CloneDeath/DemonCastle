using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class CharacterInfo : FileInfo<CharacterFile>, IListableInfo, INotifyPropertyChanged {
	public CharacterInfo(FileNavigator<CharacterFile> file) : base(file) {
		Animations = new AnimationInfoCollection(file, Resource.Animations);
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

	public string IdleAnimation {
		get => Resource.IdleAnimation;
		set {
			Resource.IdleAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string WalkAnimation {
		get => Resource.WalkAnimation;
		set {
			Resource.WalkAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string JumpAnimation {
		get => Resource.JumpAnimation;
		set {
			Resource.JumpAnimation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2 Size => new(Resource.Width, Resource.Height);

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	#endregion
}