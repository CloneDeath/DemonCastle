using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class MonsterInfo : FileInfo<MonsterFile>, IListableInfo, INotifyPropertyChanged {
	public MonsterInfo(FileNavigator<MonsterFile> file) : base(file) {
		Animations = new AnimationInfoCollection(file, Resource.Animations);
		States = new StateInfoCollection(file, Resource.States);
	}

	public Guid Id => Resource.Id;

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

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

	public Guid InitialState {
		get => Resource.InitialState;
		set {
			Resource.InitialState = value;
			Save();
			OnPropertyChanged();
		}
	}

	public AnimationInfoCollection Animations { get; }
	public StateInfoCollection States { get; }

	public ISpriteDefinition PreviewSpriteDefinition {
		get {
			var state = States.FirstOrDefault(s => s.Id == InitialState);
			var animation = Animations.FirstOrDefault(a => a.Id == state?.Animation);
			return animation?.Frames.First().SpriteDefinition ?? new NullSpriteDefinition();
		}
	}

	public Texture2D PreviewTexture => new AtlasTexture
		{ Atlas = PreviewSpriteDefinition.Texture, Region = PreviewSpriteDefinition.Region };

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