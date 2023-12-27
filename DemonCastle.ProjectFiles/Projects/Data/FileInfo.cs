using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public interface IFileInfo {
	public string FileName { get; }
	public string Directory { get; }
	public void Save();
}

public class FileInfo<TFile> : IFileInfo, INotifyPropertyChanged {
	protected FileNavigator<TFile> File { get; }

	protected TFile Resource => File.Resource;
	public string FileName => File.FileName;
	public string Directory => File.Directory;

	public FileInfo(FileNavigator<TFile> file) {
		File = file;
	}

	public void Save() => File.Save();

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