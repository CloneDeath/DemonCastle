using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace DemonCastle.ProjectFiles.Projects;

public abstract class ObservableCollectionInfo<TInfo, TData> : IObservableCollection<TInfo> {
	protected List<TData> DataItems { get; }
	protected ObservableCollection<TInfo> InfoItems { get; }

	protected ObservableCollectionInfo(List<TData> frames) {
		DataItems = frames;

		InfoItems = new ObservableCollection<TInfo>(frames.Select(CreateInfo));
		InfoItems.CollectionChanged += InfoItems_OnCollectionChanged;
	}

	protected abstract void Save();
	protected abstract TInfo CreateInfo(TData data, int index);

	public TInfo Add(TData data) {
		DataItems.Add(data);
		Save();
		var info = CreateInfo(data, InfoItems.Count);
		InfoItems.Add(info);
		return info;
	}

	public void RemoveAt(int index) {
		DataItems.RemoveAt(index);
		Save();
		InfoItems.RemoveAt(index);
	}

	public void Remove(TInfo info) {
		var index = InfoItems.IndexOf(info);
		RemoveAt(index);
	}

	public TInfo this[int index] => InfoItems[index];

	#region IEnumerable
	public IEnumerator<TInfo> GetEnumerator() => InfoItems.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void InfoItems_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}