using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DemonCastle.ProjectFiles.Projects;

public class ObservableList<T> : IEnumerable<T>, INotifyCollectionChanged {
	protected List<T> Data { get; }
	protected ObservableCollection<T> Items { get; }

	public ObservableList(List<T> data) {
		Data = data;

		Items = new ObservableCollection<T>(data);
		Items.CollectionChanged += InfoItems_OnCollectionChanged;
	}

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