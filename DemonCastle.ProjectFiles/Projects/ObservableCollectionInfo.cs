using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.ProjectFiles.Projects;

public interface IInfoFactory<out TInfo, TData> {
	TInfo CreateInfo(TData data);
	TData CreateData();
}

public abstract class ObservableCollectionInfo<TInfo, TData> : IEnumerableInfo<TInfo> {
	private readonly IInfoFactory<TInfo, TData> _factory;
	protected List<TData> DataItems { get; }
	protected ObservableCollection<TInfo> InfoItems { get; }

	protected ObservableCollectionInfo(IInfoFactory<TInfo, TData> factory, List<TData> data) {
		_factory = factory;
		DataItems = data;

		InfoItems = new ObservableCollection<TInfo>(data.Select(factory.CreateInfo));
		InfoItems.CollectionChanged += InfoItems_OnCollectionChanged;
	}

	protected abstract void Save();

	public TInfo Add(TData data) {
		DataItems.Add(data);
		Save();
		var info = _factory.CreateInfo(data);
		InfoItems.Add(info);
		return info;
	}

	public TInfo AppendNew() {
		var data = _factory.CreateData();
		return Add(data);
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

	public void Move(int oldIndex, int newIndex) {
		(DataItems[newIndex], DataItems[oldIndex]) = (DataItems[oldIndex], DataItems[newIndex]);
		Save();
		InfoItems.Move(oldIndex, newIndex);
	}

	public int IndexOf(TInfo option) => InfoItems.IndexOf(option);

	public bool CanMoveUp(TInfo action) {
		var index = InfoItems.IndexOf(action);
		return index > 0;
	}

	public void MoveUp(TInfo action) {
		if (!CanMoveUp(action)) return;
		var index = InfoItems.IndexOf(action);
		Move(index, index - 1);
	}

	public bool CanMoveDown(TInfo action) {
		var index = InfoItems.IndexOf(action);
		return index < InfoItems.Count - 1;
	}

	public void MoveDown(TInfo action) {
		if (!CanMoveDown(action)) return;
		var index = InfoItems.IndexOf(action);
		Move(index, index + 1);
	}

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