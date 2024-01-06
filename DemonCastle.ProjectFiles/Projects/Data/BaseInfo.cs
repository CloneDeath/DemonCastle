using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class BaseInfo : INotifyPropertyChanged {
	protected IFileNavigator File { get; }

	public BaseInfo(IFileNavigator file) {
		File = file;
	}

	protected void Save() => File.Save();

	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SaveField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		Save();
		OnPropertyChanged(propertyName);
		return true;
	}
	#endregion
}

public class BaseInfo<T> : BaseInfo {
	protected T Data { get; }

	public BaseInfo(IFileNavigator file, T data) : base(file) {
		Data = data;
	}
}