using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class WeaponInfo : FileInfo<WeaponFile>, IListableInfo, INotifyPropertyChanged {
	public WeaponInfo(FileNavigator<WeaponFile> file) : base(file) {
		Animations = new WeaponAnimationInfoCollection(file, Resource.Animations);
	}

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public WeaponAnimationInfoCollection Animations { get; }

	public Guid GetAnimationId(string animationName) => Animations.FirstOrDefault(a => a.Name == animationName)?.Id ?? Guid.Empty;

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