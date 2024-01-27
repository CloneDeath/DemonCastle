using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects;

public class ObservableList<T> : IEnumerable<T>, INotifyCollectionChanged {
	private readonly IFileNavigator _file;
	protected List<T> Data { get; }
	protected ObservableCollection<T> Items { get; }

	public ObservableList(IFileNavigator file, List<T> data) {
		_file = file;
		Data = data;

		Items = new ObservableCollection<T>(data);
		Items.CollectionChanged += InfoItems_OnCollectionChanged;
	}

	public void Add(T data) {
		Data.Add(data);
		Save();
		Items.Add(data);
	}

	public void Remove(T data) {
		Data.Remove(data);
		Save();
		Items.Remove(data);
	}

	protected void Save() => _file.Save();

	#region IEnumerable
	public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;
	private void InfoItems_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}